using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary.Events
{
    class UserWentOffline: IEvents
    {
        private IEventSubscribe EventHandler;
        public UserWentOffline(IEventSubscribe Handler)
        {
            EventHandler = Handler;
        }
        public void Action(XmlElement Event)
        {
            
            EventHandler.HandleUserOffline(Event);
        }
    }
}
