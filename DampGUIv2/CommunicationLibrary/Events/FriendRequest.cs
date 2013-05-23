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
        private IEventSubscribe EventHandler;

        public FriendRequest(IEventSubscribe Handler)
        {
            EventHandler = Handler;
        }

        public void Action(XmlElement Event)
        {
            string requestId = "";
            var xmlNode = Event.GetElementsByTagName("From").Item(0);
            if (xmlNode != null)
                requestId = xmlNode.InnerText;

            if(ComFriend.AcceptFriend(requestId))
                EventHandler.HandleFriendRequest(Event);
        }
    }
}
