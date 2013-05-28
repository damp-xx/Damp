using System;
using DampServer.interfaces;

namespace DampServer.commands
{
    internal class UploadGameCommand : IServerCommand
    {
        public UploadGameCommand()
        {
            NeedsAuthcatication = false;
            IsPersistant = false;
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("UploadGame");
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            // WriteTofile();
        }

        private void WriteTofile(Byte[] content)
        {
        }
    }
}