using DampServer.interfaces;

namespace DampServer.commands
{
    public class LiveCommand : IServerCommand
    {
        public LiveCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = true;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("Live");
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            ConnectionManager connectionManager = ConnectionManager.GetConnectionManager();
            User userProfile = UserManagement.GetUserByAuthToken(http.Query.Get("AuthToken"));
            connectionManager.AddConnection(new Connection {UserHttp = http, UserProfile = userProfile});
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}