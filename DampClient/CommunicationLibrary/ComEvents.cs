using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ConnectionLibrary;
using DampCS;

namespace CommunicationLibrary
{
    public class ComEvents : IEvent
    {
        public static void Listen()
        {
            var client = new DampServerClient(ComLogin._ComIp);

            client.Listen(ComLogin._ComToken, new ComEvents());

        }

        public void ParseEvent(XmlElement Event)
        {
            Console.WriteLine("Handle Element: {0}", Event.InnerXml);
            
            switch (Event.GetElementsByTagName("Command").Item(0).InnerText)
            {
                case "UserWentOnline":
                    break;
                case "UserWentOffline":
                    break;
                case "FriendsRequest":
                    break;
                case "AcceptFriend":
                    break;
                case "ChatRecieved":
                    Console.WriteLine(Event.GetElementsByTagName("Message").Item(0).InnerText);
                    break;
                default:
                    Console.WriteLine("Error parseing Event: {0}", Event.InnerXml);
                    break;
            }
        }
    }
}
