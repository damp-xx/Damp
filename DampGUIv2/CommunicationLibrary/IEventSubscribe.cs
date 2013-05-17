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
        void HandleNewChatMessage(string from, string message, string fromTitle);
        void HandleUserOnline(XmlElement Event);
        void HandleUserOffline(XmlElement Event);
        void HandleFriendRequest(XmlElement Event);
        void HandleFriendAccepted(XmlElement Event);
    }
}
