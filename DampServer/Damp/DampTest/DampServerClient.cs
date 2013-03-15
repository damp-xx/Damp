using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Damp;

namespace DampTest
{
    class DampServerClient
    {
        private readonly TcpClient _tcp = new TcpClient();
        private readonly string _host;
        private readonly int _port;
        private Stream _stream;
        private string _authToken = "";

        public bool IsConnected { get; private set; }

        public DampServerClient(string host, int port = 1337)
        {
            _port = port;
            _host = host;
            IsConnected = false;

            //SendRequest("Chat", new Dictionary<string, string> {{"Message", "Hej med dig"}});
        }

        public void Login(string username, string password)
        {
            SendRequest("Login", new Dictionary<string, string>{{"Username", username}, {"Password", password}});
        }

        public bool Verify(string code)
        {
            return false;
        }

        public void Listen()
        {
            
        }

        private void Connect()
        {
            _tcp.Connect(_host, _port);
        }

        public void SendRequest(string command, Dictionary<string, string> parameters)
        {
            var query = new StringBuilder(command);
            query.Append("?");
            foreach (var parameter in parameters)
            {
                query.AppendFormat("{0}={1}&", parameter.Key, parameter.Value);
            }

            if (!string.IsNullOrEmpty(_authToken))
            {
                query.AppendFormat("authToken={0}", _authToken);

            }
            else
            {
                query.Remove(query.Length - 1, 1);
            }

            if (!IsConnected)
            {
                Connect();
                _stream = _tcp.GetStream();
            }

            var sw = new StreamWriter(_stream);

            sw.WriteLine("GET {0} HTTP/1.1", HttpUtility.UrlPathEncode("/"+command+query));
            sw.WriteLine();

            var sr = new StreamReader(_stream);
            var contentLenght = 0;
            string contentType;

            while (true)
            {
                var l = sr.ReadLine();
                string[] ll ;
                char[] splitDelimiter = {Convert.ToChar(": ")};

                ll = l.Split(splitDelimiter, 2);

                if (ll[0].Equals("Content-Length"))
                {
                    contentLenght = int.Parse(ll[1]);
                    break;
                }
                if (ll[0].Equals("Content-Type")) contentType = ll[1];


                Console.WriteLine(l);
            }

            var buffer = new char[32*1024];

            for (var i = 0; i < contentLenght;)
            {
                sr.Read(buffer, i, buffer.Length);
                Console.WriteLine(buffer);
                i += buffer.Length;
            }

        }
    }
}
