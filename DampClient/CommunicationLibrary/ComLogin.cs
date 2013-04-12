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
        private static string _ComToken;
        private static string _ComIp = "10.20.255.127";
        public static bool Authenticate(string username, string password)
        {
            if (username == "" || password == "")
                return false;
            
            var client = new DampServerClient(_ComIp);
            
            var ResultOfLogin = client.Login(username, password, out _ComToken);
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
