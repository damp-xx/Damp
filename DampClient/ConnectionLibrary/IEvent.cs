using System.Xml;

namespace ConnectionLibrary
{
    public interface IEvent
    {
        void ParseEvent(XmlElement Event);
    }
}
