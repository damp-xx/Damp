using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary.Events
{
    class FriendRequest: IEvents
    {
        public void Action(XmlElement Event)
        {
            Console.WriteLine("Friendrequest from: {0} please accept or decline this", Event.GetElementsByTagName("From").Item(0).InnerText);
        }
    }
}
