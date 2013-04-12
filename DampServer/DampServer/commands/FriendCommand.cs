using System;
using System.Data;
using System.Data.SqlClient;
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
                _client.SendXmlResponse(new ErrorXmlResponse { Message = "Internal Server Error!!" });
                return;
            }

            db.Close();

            NotifyUser(new FrendRequestResponse { From = friend.UserId }, friend.UserId);
            _client.SendXmlResponse(new StatusXmlResponse {Code = 200, Command = "AddFriend", Message = "Friend request added"});

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
