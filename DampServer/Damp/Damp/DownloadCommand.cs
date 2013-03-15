namespace Damp
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

        public void HandleCommand(Http http, string cmd = null)
        {
            http.SendFileResponse("download.txt");
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}