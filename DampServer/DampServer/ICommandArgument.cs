using System.Collections.Specialized;

namespace Damp
{
    public interface ICommandArgument
    {
        NameValueCollection Query { get; }
        void SendFileResponse(string filename);
        void SendXmlResponse(XmlResponse obj);
        bool IsConnected { get; }

    }
}