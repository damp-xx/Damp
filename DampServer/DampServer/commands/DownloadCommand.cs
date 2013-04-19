using DampServer.interfaces;

namespace DampServer.commands
{
    public class DownloadCommand : IServerCommand
    {
        public DownloadCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("Download");
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            http.SendFileResponse("download.txt");
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}