using System.Collections.Specialized;

namespace DampServer.interfaces
{
    public interface ICommandArgument
    {
        NameValueCollection Query { get; set; }
        void SendFileResponse(string filename);
        void SendXmlResponse(XmlResponse obj);
        bool IsConnected { get; }

    }
}