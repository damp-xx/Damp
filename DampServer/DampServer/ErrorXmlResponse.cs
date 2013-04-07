#region

using System.Xml.Serialization;

#endregion

namespace Damp
{
    [XmlRoot(ElementName = "Error")]
    public class ErrorXmlResponse : XmlResponse
    {
        public string Message { get; set; }
    }
}