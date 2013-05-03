using System.Xml;

namespace ConnectionLibrary
{
    public interface IEventParser
    {
        void ParseEvent(XmlElement Event);
    }
}
