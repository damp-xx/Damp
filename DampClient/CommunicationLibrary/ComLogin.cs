using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampCS;

namespace CommunicationLibrary
{
    class ComLogin
    {
        public static string _ComToken { get; private set; }
        public static string _ComIp { get; private set; }
        public static bool Authenticate(string username, string password)
        {
            _ComIp = "10.20.255.127";
            if (username == "" || password == "")
                return false;
            
            var client = new DampServerClient(_ComIp);
            string _TempComToken;
            var ResultOfLogin = client.Login(username, password, out _TempComToken);
            _ComToken = _TempComToken;
            Console.WriteLine("Token after login: {0}", _ComToken);
            return ResultOfLogin;
        }

        public static bool ForogtAccount(string email)
        {
            if (email == "")
                return false;

            var client = new DampServerClient(_ComIp);
            Console.WriteLine("Token before send Forgotten Email: {0}", _ComToken);
            var ResultFromServerXml = client.SendRequest("ForgottenPassword", new Dictionary<string, string> {{"Email", email}}, _ComToken);
            
            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
