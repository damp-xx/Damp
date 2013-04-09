#region

using System.Xml.Serialization;

#endregion

namespace DampServer
{
    [XmlRoot(ElementName = "Error")]
    public class ErrorXmlResponse : XmlResponse
    {
        public string Message { get; set; }
    }
}