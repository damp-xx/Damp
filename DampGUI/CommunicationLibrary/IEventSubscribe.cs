using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommunicationLibrary
{
    public interface IEventSubscribe
    {
        void HandleNewChatMessage(string unknown, string @from, string message);
        void HandleUserOnline(XmlElement Event);
        void HandleUserOffline(XmlElement Event);
    }
}
