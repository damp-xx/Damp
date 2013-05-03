using DampServer.interfaces;

namespace DampServer
{
    public class Connection : IConnection
    {
        public User UserProfile { get; set; }
        public ICommandArgument UserHttp { get; set; }
    }
}