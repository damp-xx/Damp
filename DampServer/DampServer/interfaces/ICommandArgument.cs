using System.Collections.Specialized;

namespace DampServer.interfaces
{
    public interface ICommandArgument
    {
        NameValueCollection Query { get; set; }
        bool IsConnected { get; }
        void SendFileResponse(string filename);
        void SendXmlResponse(XmlResponse obj);
    }
}