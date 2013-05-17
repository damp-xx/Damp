#region

using System.Xml.Serialization;

#endregion

namespace DampServer.responses
{
    [XmlRoot(ElementName = "Status")]
    public class StatusXmlResponse : XmlResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        public long From { get; set; }
        public string To { get; set; }

        public string FromName { get; set; }

        // public T Content { get; set; }
    }
}