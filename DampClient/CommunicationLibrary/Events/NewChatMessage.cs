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
        public void Action(XmlElement Event)
        {
            Console.WriteLine("New cht message from: {0}", Event.GetElementsByTagName("From").Item(0).InnerText);
            Console.WriteLine("Content: {0}", Event.GetElementsByTagName("Message").Item(0).InnerText);
        }
    }
}
