using System.Collections.Generic;

namespace DampServer.interfaces
{
    public interface INotify
    {
        List<XmlResponse> Notify(IUser user);
    }
}