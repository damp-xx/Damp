using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Xml;

namespace DampCS
{
    public class DampServerClient
    {
        private readonly string _host;
        private readonly int _port;
        public bool IsAuthenticated { get; private set; }

        private readonly static object _lock = new object();
        private static string _authToken = "1337";

        public DampServerClient(string host, int port = 1337)
        {
            _port = port;
            _host = host;
            IsConnected = false;
        }

        public bool IsConnected { get; private set; }

        public bool Login(string username, string password)
        {
            var xml= SendRequest("Login", new Dictionary<string, string> {{"Username", username}, {"Password", password}});

            if (xml.Name.Equals("Status"))
            {
                var xmlNode = xml.GetElementsByTagName("Message").Item(0);

                if (xmlNode != null)
                {
                    _authToken = xmlNode.InnerText;
                    return true;
                }
            }

            return false;
        }


        public void Listen()
        {
            // @TODO run in seperate thread
            Run();
        }


        private void Run()
        {
            var tcp = new TcpClient();
            SendRequestWIthOutParse("Live", new Dictionary<string, string>(),tcp);

            while (true)
            {
               var element = ParseResponse(tcp.GetStream());
                Handle(element);
            }
        }

        private void Handle(XmlElement element)
        {
            Console.WriteLine("Handle Element: {0}", element.Name);
        }

        public XmlElement SendRequest(string command, Dictionary<string, string> parameters)
        {
            var tcp = new TcpClient();
            return SendRequestWIthOutParse(command, parameters, tcp);
            tcp.Close();
        }

        private XmlElement SendRequestWIthOutParse(string command, Dictionary<string, string> parameters, TcpClient tcp)
        {   
                var query = new StringBuilder(command);
                query.Append("?");
                foreach (var parameter in parameters)
                {
                    query.AppendFormat("{0}={1}&", parameter.Key, HttpUtility.UrlEncode(parameter.Value));
                }

                if (!string.IsNullOrEmpty(_authToken))
                {
                    query.AppendFormat("authToken={0}", _authToken);
                }
                else
                {
                    query.Remove(query.Length - 1, 1);
                }

               
                tcp.Connect(_host, _port);
                var stream = tcp.GetStream();
                

                var sw = new StreamWriter(stream) {AutoFlush = true};
                string qq = HttpUtility.UrlPathEncode("/" + query);
                Console.WriteLine("SENT GET: {0}", qq);

                sw.WriteLine("GET {0} HTTP/1.1", qq);
                sw.WriteLine();

                var el = ParseResponse(stream);
                
            return el;

        }

        private XmlElement ParseResponse(Stream stream)
        {
            var sr = new StreamReader(new BufferedStream(stream));
            int contentLenght = 0;
            string contentType;

            while (true)
            {
                string l = sr.ReadLine();

                if (l == null) return null;
                if (l.Equals("")) break;

                string[] ll;
                char[] splitDelimiter = { Convert.ToChar(":") };
                Console.WriteLine(l);
                ll = l.Split(splitDelimiter, 2);

                if (ll[0].Equals("Content-Length"))
                {
                    contentLenght = int.Parse(ll[1]);
                }
                if (ll[0].Equals("Content-Type")) contentType = ll[1];
            }

            var buffer = new char[contentLenght];

      

            var data = new char[contentLenght];
            var bytes = sr.Read(data, 0, data.Length);
         
            Console.WriteLine("Received: {0}", new string(data));    
 
            if(bytes!=contentLenght) Console.WriteLine("WTF RECEIVED BYTES NOT THOSE EXPECTED!! REC: {0}. EXC: {1}", bytes, contentLenght);

            var xDoc = new XmlDocument();
            xDoc.LoadXml(new string(data));
            var element = xDoc.DocumentElement;

            return element;

        }
    }
}