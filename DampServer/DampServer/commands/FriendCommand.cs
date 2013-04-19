using System;
using System.Data;
using System.Data.SqlClient;
using DampServer.interfaces;
using DampServer.responses;

namespace DampServer.commands
{
    class FriendCommand : IServerCommand
    {
        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
        private ICommandArgument _client;

        public FriendCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string cmd)
        {
            throw new NotImplementedException();
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            _client = http;

            switch (cmd)
            {
                case "AddFriend":
                    HandleAddFriend();
                    break;
                case "AcceptFriend":
                    HandleAcceptFriend();
                    break;
                case "RemoveFriend":
                    HandleRemoveFriend();
                    break;
                   

            }
        }

        private void HandleAddFriend()
        {
            var db = new Database();

            // @TODO CHECK IF USER EXISTS!

            if(string.IsNullOrEmpty(_client.Query.Get("Friend")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse {Message = "Missing argurments!"});
                return;
            }

            User friend;

            try
            {
                 friend = UserManagement.GetUserById(_client.Query.Get("Friend"));
            }
            catch (UserNotFoundException)
            {
               _client.SendXmlResponse(new ErrorXmlResponse {Message = "Friend not found!"});
                return;
            }

            db.Open();

            var sqlCmd = db.GetCommand();
            var user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            // @TODO MAKE SURE I CAN ONLY HAVE ONE FRIEND REQUEST
            sqlCmd.CommandText = "INSERT INTO FriendRequests (\"User\", \"Friend\") VALUES (@userid, @friendid)";
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            sqlCmd.Parameters.Add("@friendid", SqlDbType.BigInt).Value = friend.UserId;

            try
            {
                sqlCmd.ExecuteReader();
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(e.Message);
                _client.SendXmlResponse(new ErrorXmlResponse { Message = "Internal Server Error!! #2321114" });
                return;
            }

            db.Close();

            NotifyUser(new FrendRequestResponse { From = friend.UserId }, friend.UserId);
            _client.SendXmlResponse(new StatusXmlResponse {Code = 200, Command = "AddFriend", Message = "Friend request added"});

        }

        private void HandleAcceptFriend()
        {
            Int64 friendid;
            
            if (string.IsNullOrEmpty(_client.Query.Get("Friend")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing or invalid argument #3312"
                    });
                return;
            }
            else
            {
                friendid = Int64.Parse(_client.Query.Get("Friend"));
            }


            Database db = new Database();

            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            sqlCmd.CommandText = "SELECT * FROM FriendRequests WHERE \"User\" = @userid AND Friend = @friendid";
            
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value =
                UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken")).UserId;

            sqlCmd.Parameters.Add("@friendid", SqlDbType.BigInt).Value = friendid;
            SqlDataReader r = sqlCmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Close();
                sqlCmd.CommandText = "DELETE FROM FriendRequests WHERE \"User\" = @userid AND Friend = @friendid";
                sqlCmd.ExecuteNonQuery();

                sqlCmd.CommandText = "INSERT INTO Friends (\"userid\", \"userid1\") VALUES (@userid, @friendid)";
                sqlCmd.ExecuteNonQuery();

                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Code = 200,
                        Command = "AcceptFriend",
                        Message = "Friend Accepted!"
                    });
            }
            else
            {
                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Code = 301,
                        Command = "AcceptFriend",
                        Message = "Friend not found!"
                    });
            }

            db.Close();        }

        private void HandleRemoveFriend()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Friend")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurment!! #211764"
                    });
                return;
            }

            long frindid = long.Parse(_client.Query.Get("Friend"));

            Database db = new Database();

            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "DELETE FROM Friends WHERE userid = @userid AND userid1 = @friendid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value =
                UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken")).UserId;
            cmd.Parameters.Add("@friendid", SqlDbType.BigInt).Value = frindid;

            cmd.ExecuteNonQuery();

            db.Close();

            _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "RemoveFriend",
                    Message = "Friend was removed!"
                });
        }

        private void NotifyUser(XmlResponse xml, long userid)
        {
            IConnection receiver = null;
            try
            {
                receiver =
                    ConnectionManager.GetConnectionManager().GetConnectionByUserId(userid);
            }
            catch (Exception e)
            {
                Console.WriteLine("ChatCommandP Exception 22111: {0}", e.Message);
            }

            if (receiver != null)
            {
                Console.WriteLine("ChatCommand: User online, sending notifying friend");
                receiver.UserHttp.SendXmlResponse(xml);
            }
            else
            {
                Console.WriteLine("ChatCommand: User not online, logging chat");
            }


        }
    }
}
