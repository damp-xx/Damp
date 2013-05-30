using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary.Events
{
    class FriendAccepted: IEvents
    {
        private IEventSubscribe EventHandler;
        public FriendAccepted(IEventSubscribe Handler)
        {
            EventHandler = Handler;
        }

        public void Action(XmlElement Event)
        {
            EventHandler.HandleFriendAccepted(Event);
        }
    }
}
