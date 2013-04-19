using System.Xml.Serialization;

namespace DampServer.responses
{
    [XmlRoot(ElementName = "FriendRequest")]
    public class FrendRequestResponse : XmlResponse
    {
        public long From { get; set; }
    }
}
