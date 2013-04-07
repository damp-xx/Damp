namespace Damp
{
    public interface IServerCommand
    {
        bool NeedsAuthcatication { get; }
        bool IsPersistant { get; }
        bool CanHandleCommand(string cmd);
        void HandleCommand(Http http, string cmd = null);
    }
}