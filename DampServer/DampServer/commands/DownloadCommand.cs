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
            if (string.IsNullOrEmpty(http.Query.Get("Object")))
            {
                http.SendXmlResponse(new ErrorXmlResponse {Message = "Missing arguments"});
                return;
            }

            http.SendFileResponse(@"../../public/" + http.Query.Get("Object"));
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}