using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using DampServer.interfaces;
using DampServer.responses;

namespace DampServer.commands
{
    internal class FriendCommand : IServerCommand, INotify
    {
        private ICommandArgument _client;

        public FriendCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public List<XmlResponse> Notify(IUser user)
        {
            Database db = new Database();

            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            sqlCmd.CommandText = "SELECT * FROM Notifications WHERE userid = @userid ";

            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r = sqlCmd.ExecuteReader();
            List<XmlResponse> responses = new List<XmlResponse>();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    responses.Add(new StatusXmlResponse
                        {
                            Code = (int) r["code"],
                            Command = (string) r["command"],
                            From = (long) r["from"],
                            To = ((long) r["to"]).ToString(CultureInfo.InvariantCulture),
                            Message = (string) r["message"]
                        });

                    Database db2 = new Database();
                    db2.Open();

                    SqlCommand sql = db2.GetCommand();
                    sql.CommandText = "DELETE FROM Notifications WHERE id = @id";
                    sql.Parameters.Add("@id", SqlDbType.BigInt).Value = (long) r["id"];
                    sql.ExecuteNonQuery();
                    db2.Close();
                }
            }

            r.Close();
            db.Close();

            return responses;
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }

        public bool CanHandleCommand(string cmd)
        {
            return false;
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
            Database db = new Database();

            if (string.IsNullOrEmpty(_client.Query.Get("Friend")))
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

            SqlCommand sqlCmd = db.GetCommand();
            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

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
                _client.SendXmlResponse(new ErrorXmlResponse {Message = "Internal Server Error!! #2321114"});
                return;
            }

            db.Close();

            NotifyUser(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "FriendRequest",
                    To = friend.UserId.ToString(CultureInfo.InvariantCulture),
                    From = user.UserId
                }, friend.UserId);

            _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "AddFriend",
                    Message = "Friend request added",
                    To = friend.UserId.ToString(CultureInfo.InvariantCulture),
                    From = user.UserId
                });
        }

        private void HandleAcceptFriend()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Friend")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing or invalid argument #3312"
                    });
                return;
            }

            long friendid = Int64.Parse(_client.Query.Get("Friend"));


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

                NotifyUser(
                    new StatusXmlResponse
                        {
                            Code = 200,
                            To = friendid.ToString(CultureInfo.InvariantCulture),
                            Command = "FriendAccepted",
                            Message = friendid.ToString(CultureInfo.InvariantCulture),
                            From = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken")).UserId
                        }, friendid);
            }
            else
            {
                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Code = 404,
                        Command = "AcceptFriend",
                        Message = "Friend not found!"
                    });
            }

            db.Close();
        }

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

        private void NotifyUser(StatusXmlResponse xml, long userid)
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
                Database db = new Database();

                db.Open();

                SqlCommand sqlCmd = db.GetCommand();

                if (string.IsNullOrEmpty(xml.Message)) xml.Message = "";
                if (string.IsNullOrEmpty(xml.To)) xml.To = "0";


                sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = userid;
                sqlCmd.Parameters.Add("@code", SqlDbType.Int).Value = xml.Code;
                sqlCmd.Parameters.Add("@fff", SqlDbType.BigInt).Value = xml.From;
                sqlCmd.Parameters.Add("@bbb", SqlDbType.BigInt).Value = long.Parse(xml.To);
                sqlCmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = xml.Message;
                sqlCmd.Parameters.Add("@command", SqlDbType.NVarChar).Value = xml.Command;

                // get user
                sqlCmd.CommandText = "INSERT INTO Notifications ([from], [to], userid, message, command, code)" +
                                     " VALUES(@fff, @bbb, @userid, @message, @command, @code)";

                sqlCmd.ExecuteNonQuery();


                SqlDataReader r = sqlCmd.ExecuteReader();


                r.Close();
                db.Close();
            }
        }
    }
}