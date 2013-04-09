namespace DampServer
{
    public interface IConnection
    {
        User UserProfile { get; set; }
        ICommandArgument UserHttp { get; set; }
    }
}