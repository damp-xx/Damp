using System.Collections.Specialized;

namespace DampServer
{
    public interface ICommandArgument
    {
        NameValueCollection Query { get; set; }
        void SendFileResponse(string filename);
        void SendXmlResponse(XmlResponse obj);
        bool IsConnected { get; }

    }
}