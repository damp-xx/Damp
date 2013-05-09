using System;
using System.Xml;
using CommunicationLibrary.Events;
using ConnectionLibrary;
using DampCS;

namespace CommunicationLibrary
{
    public class ComEvents : IEventParser
    {
        private static IEventSubscribe _eventHandler;
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
                        command = new UserWentOnline(_eventHandler);
                        break;
                    case "UserWentOffline":
                        command = new UserWentOffline(_eventHandler);
                        
                        break;
                    case "FriendRequest":
                        command = new FriendRequest();
                        break;
                    case "FriendAccepted":
                        command = new FriendAccepted();
                        break;
                    case "ChatRecieved":
                        command = new NewChatMessage(_eventHandler);
                        break;
                    default:
                        Console.WriteLine("Error parseing Event: {0}", Event.InnerXml);
                        break;
                }
                try
                {
                    if (command != null) command.Action(Event);
                }
                catch (NullReferenceException nEx)
                {
                    Console.WriteLine("Event not recogniced ....");
                }
            }
        }

        public static void EventSubscrie(IEventSubscribe subscriber)
        {
            _eventHandler = subscriber;
        }
    }
}
