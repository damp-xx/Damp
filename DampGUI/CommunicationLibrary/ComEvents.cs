using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CommunicationLibrary.Events;
using ConnectionLibrary;
using DampCS;

namespace CommunicationLibrary
{
    public class ComEvents : IEventParser
    {
        private static IEventSubscribe ChatHandler;
        public static void Listen()
        {
            var client = new DampServerClient(ComLogin._ComIp);

            client.Listen(ComLogin._ComToken, new ComEvents());

        }

        public void ParseEvent(XmlElement Event)
        {
            IEvents command = null;
            Console.WriteLine("Handle Element: {0}", Event.InnerXml);
            if (Event.Name.Equals("Status"))
            {
                switch (Event.GetElementsByTagName("Command").Item(0).InnerText)
                {
                    case "UserWentOnline":
                        command = new UserWentOnline();
                        break;
                    case "UserWentOffline":
                        command = new UserWentOffline();
                        break;
                    case "FriendRequest":
                        command = new FriendRequest();
                        break;
                    case "FriendAccepted":
                        command = new FriendAccepted();
                        break;
                    case "ChatReceived":
                        command = new NewChatMessage(ChatHandler);
                        break;
                    default:
                        Console.WriteLine("Error parseing Event: {0}", Event.InnerXml);
                        break;
                }
                try
                {
                    command.Action(Event);
                }
                catch (NullReferenceException NEx)
                {
                    Console.WriteLine("Event not recogniced ....");
                }
            }
        }

        public static void EventSubscrie(IEventSubscribe Subscriber)
        {
            ChatHandler = Subscriber;
        }
    }
}
