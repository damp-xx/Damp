/**
 * @file   	Http.cpp
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file implements a HTTP Server for DAMP Server
 * @section	LICENSE GPL 
 */

#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml.Serialization;

#endregion

namespace DampServer
{
    /**
     * Http
     * @brief A class to handle all HTTP communication
     */
    public class Http : ICommandArgument
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _headersToSend = new Dictionary<string, string>();
        private readonly SslStream _network;
        private readonly TcpClient _socket;
        private static readonly X509Certificate ServerCertificate = new X509Certificate2("server.pfx", "");

        private bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 & part2)
                return false;
            else
                return true;
        }

        /**
         * IsConnected
         * 
         * @brief The property display if the client is connected to the HTTP/TCP Socket
         */
        public bool IsConnected
        {
            get { return SocketConnected(_socket.Client); }
        }

        /**
         * Http
         * 
         * @brief Constructor
         * @param TcpClient A TcpClient connected to a client
         */
        public Http(TcpClient s)
        {
            _socket = s;
            _network = new SslStream(_socket.GetStream(),true);

            try
            {
                _network.AuthenticateAsServer(ServerCertificate, false, SslProtocols.Tls, false);
            }
            catch (Exception e)
            {
                
                Logger.Log(e.Message);
                return;
                
            }

            // SSL Authenticated
            if (_network.IsAuthenticated)

            ParseGet();
        }

        public Byte[] Content { get; private set; }
        public string Type { get; private set; }
        public string Path { get; private set; }
        public string HttpVersion { get; private set; }
        public long Length { get; private set; }
        public string Status { get; private set; }
        public NameValueCollection Query { get;  set; }

        /**
         * GetHeader
         * 
         * @brief Method to get a HTTP Header
         */
        public string GetHeader(string key)
        {
            return _headers[key];
        }

        /**
         * AddHeader
         * 
         * @brief Method to add a HTTP header to the HTTP response
         */
        public void AddHeader(string name, string value)
        {
            _headersToSend.Add(name, value);
        }

        /**
         * ParseGet
         * 
         * @brief private method to parse HTTP GET requests
         */
        private void ParseGet()
        {
            var sr = new StreamReader(new BufferedStream(_network));

            // @TODO WTF, WHY IS THIS NEEDED WITH PUTTY!
            // sr.ReadLine();

            string readLine = sr.ReadLine();
            if (string.IsNullOrEmpty(readLine))
            {
                throw new InvalidHttpRequestException("No GET line");
            }

            char[] sD = {' '};
            string[] line = readLine.Split(sD, 3);

            try
            {
                Type = line[0];

                if (line[1].IndexOf("?", StringComparison.Ordinal) != 0)
                {
                    Path = line[1].Substring(1, line[1].IndexOf("?", StringComparison.Ordinal) - 1);
                }

                Query =
                    HttpUtility.ParseQueryString(line[1].Substring(line[1].IndexOf("?", StringComparison.Ordinal) + 1));
                HttpVersion = line[2];
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidHttpRequestException("Parsing GET line failed!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception 3: {0}", e.Message);
            }

            while (true)
            {
                readLine = sr.ReadLine();
                if (string.IsNullOrEmpty(readLine))
                {
                    break;
                }

                line = readLine.Split(sD, 2);
                _headers.Add(line[0], line[1]);
            }

            if (Type.Equals("POST"))
            {
                ParsePost(sr);
            }
        }

        /**
         * ParsePost
         * 
         * @brief private method to parse HTTP POST requests
         */
        private void ParsePost(TextReader sr)
        {
            
            var contentLenght = int.Parse( GetHeader("Content-Length"));
            Content = new Byte[contentLenght];
            var data = new char[contentLenght];
            var bytes = sr.Read(data, 0, data.Length);

            Console.WriteLine("Received: {0}", contentLenght);

            if (bytes != contentLenght) Console.WriteLine("WTF RECEIVED BYTES NOT THOSE EXPECTED!! REC: {0}. EXC: {1}", bytes, contentLenght);
 
        }

        /**
         * SendXmlResponse
         * 
         * @brief method to send a XML response object to the client
         */
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendXmlResponse(XmlResponse obj)
        {
            var serializer = new XmlSerializer(obj.GetType());
            var ns = new MemoryStream();
            serializer.Serialize(ns, obj);

            Length = ns.Length;
            Type = "text/xml";
            Status = "200 OK";

            SendResponseHeader();

            if (!_network.CanWrite) return;

            try
            {
                _network.Write(ns.GetBuffer(), 0, (int)ns.Length);
                _network.Flush();
            }
            catch (Exception e)
            {
                
                Logger.Log(e.Message);
            }
        }

        /**
         * SendResponseHeader
         * 
         * @brief private helper method to send standard HTTP headers
         */
        private void SendResponseHeader()
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(_network) {AutoFlush = true};
            }
            catch (ArgumentException e)
            {
                
                Logger.Log(e.Message);
                return;
            }

            try
            {
                sw.WriteLine("HTTP/1.1 {0}", Status);
                sw.WriteLine("Content-Type: {0}", Type);
                sw.WriteLine("Content-Length: {0}", Length);

                foreach (KeyValuePair<string, string> s in _headersToSend)
                {
                    sw.WriteLine("{0}: {1}", s.Key, s.Value);
                }

                sw.WriteLine();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
            }           
        }

        /**
         * SendFileResponse
         * 
         * @brief send a file to the client
         */
        public void SendFileResponse(string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                Length = fs.Length;
                Type = MediaTypeNames.Application.Octet;
                AddHeader("Content-disposition", "attachment; filename=largefile.txt");
                Status = "200 OK";

                SendResponseHeader();

                var buffer = new byte[64*1024];
                using (var bw = new BinaryWriter(_network))
                {
                    int read;
                    while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        bw.Write(buffer, 0, read);
                        bw.Flush(); //seems to have no effect
                    }

                    bw.Close();
                }
            }
        }
    }
}