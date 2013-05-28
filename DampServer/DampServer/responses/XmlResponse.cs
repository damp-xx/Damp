#region

using System;
using System.Xml.Serialization;

#endregion

namespace DampServer
{
    [Serializable]
    [XmlRoot(ElementName = "Damp")]
    public abstract class XmlResponse
    {
        public DateTime Date { get; set; }
    }
}