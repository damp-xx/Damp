namespace Damp
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

        public void HandleCommand(Http http, string cmd = null)
        {
            ConnectionManager connectionManager = ConnectionManager.GetConnectionManager();
            User userProfile = UserManagement.GetUser(http.Query.Get("authToken"));
            connectionManager.AddConnection(new Connection {UserHttp = http, UserProfile = userProfile});
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}