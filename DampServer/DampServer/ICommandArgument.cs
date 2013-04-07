using System.Collections.Specialized;

namespace DampServer
{
    public interface ICommandArgument
    {
        NameValueCollection Query { get; }
        void SendFileResponse(string filename);
        void SendXmlResponse(XmlResponse obj);
        bool IsConnected { get; }

    }
}