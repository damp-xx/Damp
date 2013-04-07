#region

using System.Xml.Serialization;

#endregion

namespace Damp
{
    [XmlRoot(ElementName = "Chat")]
    public class ChatXmlResponse : XmlResponse
    {
        public string Message { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}