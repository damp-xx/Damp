namespace DampServer.interfaces
{
    public interface IConnection
    {
        User UserProfile { get; set; }
        ICommandArgument UserHttp { get; set; }
    }
}