#region

using System.Xml.Serialization;

#endregion

namespace Damp
{
    [XmlRoot(ElementName = "Status")]
    public class StatusXmlResponse : XmlResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Command { get; set; }
        // public T Content { get; set; }
    }
}