using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary.Events
{
    class UserWentOnline: IEvents
    {
        private IEventSubscribe EventHandler;
        public UserWentOnline(IEventSubscribe Handler)
        {
            EventHandler = Handler;
        }
        public void Action(XmlElement Event)
        {
            

            EventHandler.HandleUserOnline(Event);
        }
    }
}
