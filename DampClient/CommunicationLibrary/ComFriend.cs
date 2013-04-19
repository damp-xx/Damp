using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using DampCS;

namespace CommunicationLibrary
{

    class ComFriend
    {
        private static string _ComToken;
        private static string _ComIp;
        public static XmlElement GetFriendList()
        {
            var client = new DampServerClient(_ComIp);
            Console.WriteLine("Token before send Forgotten Email: {0}", _ComToken);
            var ResultFromServerXml = client.SendRequest("GetMyFriends", null, _ComToken);

            if (ResultFromServerXml.Name.Equals("Status"))
            {
                var xmlNode = ResultFromServerXml.GetElementsByTagName("Code").Item(0);

                if (xmlNode.InnerText == "200")
                {
                    return ResultFromServerXml;
                }
            }
            return null;
        }

        public static bool SendChatMessage(string message)
        {
            var client = new DampServerClient(_ComIp);
            //Console.WriteLine("Token before send Forgotten Email: {0}", _ComToken);
            var ResultFromServerXml = client.SendRequest("Chat", new Dictionary<string, string>{{"Message", message}}, _ComToken);

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
        /*
        public static void AddFriend()
        {
            
        }

        public static void RemoveFriend()
        {
            
        }*/
    }
}
