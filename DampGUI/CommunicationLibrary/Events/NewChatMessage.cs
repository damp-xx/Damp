using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary.Events
{
    class NewChatMessage : IEvents
    {
        private IEventSubscribe ChatHandler;
        public NewChatMessage(IEventSubscribe Handler)
        {
            ChatHandler = Handler;
        }
        public void Action(XmlElement Event)
        {
            Console.WriteLine("NewChatMessage");
            string from = "";
            var xmlNode = Event.GetElementsByTagName("From").Item(0);
            if (xmlNode != null)
                from  = xmlNode.InnerText;

            string message = "";
            var xmlNode1 = Event.GetElementsByTagName("Message").Item(0);
            if (xmlNode1 != null)
                message = xmlNode1.InnerText;
            ChatHandler.HandleNewChatMessage(from, message);
            Console.WriteLine("NewChatMessage Done");
        }
    }
}