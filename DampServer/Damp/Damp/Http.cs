#region

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Serialization;

#endregion

namespace Damp
{
    /* Http
     * @brief A class to handle all HTTP communication
     */

    public class Http
    {
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _headersToSend = new Dictionary<string, string>();
        private readonly NetworkStream _network;
        private readonly Socket _socket;

        public Http(Socket s)
        {
            _socket = s;
            Socket = s;
            _network = new NetworkStream(_socket);
            Parse();
        }


        public string Type { get; set; }
        public string Path { get; private set; }
        public string HttpVersion { get; private set; }
        public long Length { get; set; }
        public string Status { get; set; }

        public NameValueCollection Query { get; private set; }

        public Socket Socket { get; private set; }

        public string GetHeader(string key)
        {
            return _headers[key];
        }

        public void AddHeader(string name, string value)
        {
            _headersToSend.Add(name, value);
        }

        private void Parse()
        {
            StreamReader sr = new StreamReader(_network);

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
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SendXmlResponse(XmlResponse obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            MemoryStream ns = new MemoryStream();
            serializer.Serialize(ns, obj);

            Length = ns.Length;
            Type = "text/xml";
            Status = "200 OK";


            SendResponseHeader();

            if (!_network.CanWrite) return;

            _network.Write(ns.GetBuffer(), 0, (int) ns.Length);
            _network.Flush();
        }

        private void SendResponseHeader()
        {
            StreamWriter sw = new StreamWriter(_network) {AutoFlush = true};

            sw.WriteLine("HTTP/1.1 {0}", Status);
            sw.WriteLine("Content-Type: {0}", Type);
            sw.WriteLine("Content-Length: {0}", Length);

            foreach (KeyValuePair<string, string> s in _headersToSend)
            {
                sw.WriteLine("{0}: {1}", s.Key, s.Value);
            }

            sw.WriteLine();
        }

        public void SendFileResponse(string filename)
        {
            using (FileStream fs = File.OpenRead(filename))
            {
                Length = fs.Length;
                Type = MediaTypeNames.Application.Octet;
                AddHeader("Content-disposition", "attachment; filename=largefile.txt");
                Status = "200 OK";

                SendResponseHeader();

                byte[] buffer = new byte[64*1024];
                using (BinaryWriter bw = new BinaryWriter(_network))
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