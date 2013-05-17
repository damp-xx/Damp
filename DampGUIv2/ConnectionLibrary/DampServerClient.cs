using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Xml;
using System.Threading.Tasks;
using ConnectionLibrary;

namespace DampCS
{
    public class DampServerClient :  IDampServerClient
    {
        private readonly string _host;
        private readonly int _port;
        public bool IsAuthenticated { get; private set; }
        private string _ComToken = null;
        private readonly static object _lock = new object();
        private IEventParser HandleParse;
        //private static string _authToken = "1337";


        // The following method is invoked by the RemoteCertificateValidationDelegate. 
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            //Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

         // Do not allow this client to communicate with unauthenticated servers. 
         // return false;

            return true;
        }

        public DampServerClient(string host, int port = 1337)
        {
            _port = port;
            _host = host;
            IsConnected = false;
        }

        public bool IsConnected { get; private set; }

        public bool Login(string username, string password, out string _authToken)
        {
            var xml= SendRequest("Login", new Dictionary<string, string> {{"Username", username}, {"Password", password}}, null);

            if (xml.Name.Equals("Status"))
            {
                var xmlNode = xml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    _authToken = xml.GetElementsByTagName("Message").Item(0).InnerText;
                    return true;
                }
            }
            _authToken = null;
            return false;
        }


        public void Listen(string _authToken, IEventParser HandleParser)
        {
            _ComToken = _authToken;
            HandleParse = HandleParser;
            Task ListenTask = Task.Factory.StartNew(Run);
        }

        private void Run()
        {
            var tcp = new TcpClient();
            SendRequestWIthOutParse("Live", new Dictionary<string, string>(),tcp, _ComToken);
        }

        private void Handle(XmlElement element)
        {
            
            HandleParse.ParseEvent(element);
        }

        public XmlElement SendRequest(string command, Dictionary<string, string> parameters, string _authToken)
        {
            var tcp = new TcpClient();
            return SendRequestWIthOutParse(command, parameters, tcp, _authToken);
            tcp.Close();
        }

        private XmlElement SendRequestWIthOutParse(string command, Dictionary<string, string> parameters, TcpClient tcp, string _authToken)
        {
            SslStream stream = null;
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

            try
            {
                tcp.Connect(_host, _port);
                stream = new SslStream(
                    tcp.GetStream(),
                    false,
                    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                    null
                    );
            }
           catch (SocketException ConnectEx)
            {
               Console.WriteLine(ConnectEx);
            }

            try
            {
                stream.AuthenticateAsClient("*");
            }
            catch (Exception e)
            {
            //     Console.WriteLine("Exp: {0}", e.Message);           
            }
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(stream) {AutoFlush = true};
            }
            catch (Exception)
            {
                
                Console.WriteLine("Server down please contact admin");
                return null;
            }
            
            string qq = HttpUtility.UrlPathEncode("/" + query);
            //Console.WriteLine("SENT GET: {0}", qq);

            sw.WriteLine("GET {0} HTTP/1.1", qq);
            sw.WriteLine();

            if (command.Equals("Live"))
            {
                while (true)
                {
                    var e2 = ParseResponse(stream);
                    Handle(e2);
                }
            }

                var el = ParseResponse(stream);
                
            return el;

        }

        private XmlElement ParseResponse(Stream stream)
        {
            Console.WriteLine("PARSING!!!    !!");
            var sr = new StreamReader(new BufferedStream(stream));
            int contentLenght = 0;
            string contentType;

            while (true)
            {
                try
                {
                    string l = sr.ReadLine();

                    if (l == null)
                    {
                        Console.WriteLine("Error no input in string");
                        return null;
                    }
                    if (l.Equals("")) break;

                    string[] ll;
                    char[] splitDelimiter = { Convert.ToChar(":") };
                    //  Console.WriteLine(l);
                    ll = l.Split(splitDelimiter, 2);

                    if (ll[0].Equals("Content-Length"))
                    {
                        contentLenght = int.Parse(ll[1]);
                    }

                    if (ll[0].Equals("Content-Type")) contentType = ll[1];
                }
                catch (IOException)
                {
                    
                    Console.WriteLine("Server lost connection");
                    Environment.Exit(-1);
                }
            }

            var data = new char[contentLenght];
            var bytes = sr.Read(data, 0, data.Length);
         
            //Console.WriteLine("Received: {0}", new string(data));    
 
           // if(bytes!=contentLenght) Console.WriteLine("WTF RECEIVED BYTES NOT THOSE EXPECTED!! REC: {0}. EXC: {1}", bytes, contentLenght);

            var xDoc = new XmlDocument();
            xDoc.LoadXml(new string(data));
            var element = xDoc.DocumentElement;

            return element;
        }
    }
}