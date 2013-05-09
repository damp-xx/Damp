using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampServer.interfaces;

namespace DampServer.commands
{
    class HighScore : IServerCommand
    {
        private ICommandArgument _client;

        public HighScore()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
        public bool CanHandleCommand(string cmd)
        {
            throw new NotImplementedException();
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            _client = http;
            switch (cmd)
            {
                case "UpdateScore":
                    HandleUpdateScore();
                    break;
                case "GetScore":
                    HandleGetScore();
                    break;
            }
        }

        private void HandleGetScore()
        {
            throw new NotImplementedException();
        }

        private void HandleUpdateScore()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("GameId")) || string.IsNullOrEmpty(_client.Query.Get("Score")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments #221145"
                    });

                return;
            }

            var db = new Database();
            db.Open();

            var user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            var sql = db.GetCommand();

            sql.CommandText =   "begin tran " +
                                "update HighScore with (serializable) set score = @score "+
                                "where userid = @userid and gameid = @gameid "+

                                "if @@rowcount = 0 "+
                                "begin "+
                                "    insert HighScore (userid, gameid, score) values (@userid, @gameid, @score) "+
                                "end "+
                                "commit tran ";
            sql.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            sql.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("GameId");
            sql.Parameters.Add("@score", SqlDbType.BigInt).Value = _client.Query.Get("Score");

            sql.ExecuteNonQuery();

            _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "UpdateScore",
                    Message = "Score updated"
                });


            db.Close();
        }
    }
}
