#region

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace DampServer
{
    public class UserCommands : IServerCommand
    {
        private ICommandArgument _http;

        public UserCommands()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            return cmd.Equals("GetUserByAuthToken") || (cmd.Equals("GetMyUser"));
        }

        public void Execute(ICommandArgument http, string cmd)
        {
            _http = http;


            if (cmd.Equals("GetMyUser") && string.IsNullOrEmpty(_http.Query.Get("authToken")))
            {
                _http.SendXmlResponse(new ErrorXmlResponse {Message = "Invalid arguments #12"});
                return;
            }

            User user = new User();

            if (cmd.Equals("GetMyUser"))
            {
                user = UserManagement.GetUserByAuthToken(_http.Query.Get("authToken"));
            }
            else
            {
                user.UserId = int.Parse(_http.Query.Get("userId"));
            }

            Database db = new Database();
            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE userid = @userid";
            sqlCmd.Parameters.Add("@userid", SqlDbType.NVarChar).Value = user.UserId;
            SqlDataReader r = sqlCmd.ExecuteReader();

            if (r.Read())
            {
                user.Username = (string) r["username"];
                user.Email = (string) r["email"];              
            }
            r.Close();

            // get user games
            sqlCmd.CommandText =
                "SELECT * FROM Games WHERE gameid = (SELECT gameid FROM GameLibaray WHERE userid = @userid)";
            r = sqlCmd.ExecuteReader();

            user.Games = new List<Game>();
            while (r.Read())
            {
                Game g = new Game
                    {
                        Description =  r["description"] as string,
                        Title = (string) r["title"],
                        Id = (long) r["gameid"]
                    };

                user.Games.Add(g);
            }
            r.Close();

            // get user friends
            sqlCmd.CommandText =
                "SELECT * FROM Users WHERE userid = (SELECT userid FROM Friends WHERE userid1 = @userid)";
        //    sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            r = sqlCmd.ExecuteReader();

            user.Friends = new List<User>();
            while (r.Read())
            {

                User u = new User {Username = (string) r["username"], Email = (string) r["email"]};
                user.Friends.Add(u);

                r.Close();
            }
            r.Close();

            db.Close();

            _http.SendXmlResponse(user);
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}