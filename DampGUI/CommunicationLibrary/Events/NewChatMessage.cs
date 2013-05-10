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
            string from = "";
            var xmlNode = Event.GetElementsByTagName("From").Item(0);
            if (xmlNode != null)
                from  = xmlNode.InnerText;

            string fromTitle = "";
            var sdd = Event.GetElementsByTagName("FromName").Item(0);
            if (sdd != null)
                fromTitle = sdd.InnerText;

            string message = "";
            var xmlNode1 = Event.GetElementsByTagName("Message").Item(0);
            if (xmlNode1 != null)
                message = xmlNode1.InnerText;
            ChatHandler.HandleNewChatMessage(from, message, fromTitle);
            Console.WriteLine("NewChatMessage Done");
        }
    }
}