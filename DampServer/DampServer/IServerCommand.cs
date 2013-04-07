namespace Damp
{
    public interface IServerCommand
    {
        bool NeedsAuthcatication { get; }
        bool IsPersistant { get; }
        bool CanHandleCommand(string cmd);
        void Execute(ICommandArgument http, string cmd = null);
    }
}