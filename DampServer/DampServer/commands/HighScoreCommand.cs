using System.Collections.Generic;
using System.Data;
using System.Globalization;
using DampServer.interfaces;
using DampServer.responses;

namespace DampServer.commands
{
    public class HighScoreCommand : IServerCommand
    {
        private ICommandArgument _client;

        public HighScoreCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
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
                case "UpdateScore":
                    HandleUpdateScore();
                    break;
                case "GetScore":
                    HandleGetScore();
                    break;
                case "GetHighScore":
                    HandleGetHighScore();
                    break;
            }
        }

        private void HandleGetHighScore()
        {
            if(string.IsNullOrEmpty(_client.Query.Get("GameId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments!"
                    });
                return;

            }

            var response = new HighScoreResponse
                {
                    GameId = int.Parse(_client.Query.Get("GameId")),
                    UserScores = new List<User>()
                };

            var db = new Database();
            db.Open();

            var sql = db.GetCommand();


            sql.CommandText = "SELECT TOP 10 * FROM HighScore WHERE gameid = @gameid ORDER BY score DESC";
            sql.Parameters.Add("@gameid", SqlDbType.BigInt).Value = long.Parse(_client.Query.Get("GameId"));

            var r = sql.ExecuteReader();

            if (r.HasRows)
            {
                while (r.Read())
                {
                    response.UserScores.Add(new User { UserId = (long)r["userid"], Score = ((long)r["score"]).ToString(CultureInfo.InvariantCulture) });
                }
            }

            _client.SendXmlResponse(response);
        }

        private void HandleGetScore()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("GameId")))
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

            sql.CommandText = "SELECT * FROM HighScore WHERE userid = @userid AND gameid = @gameid";
            sql.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            sql.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("GameId");

            var r = sql.ExecuteReader();

            var s = new StatusXmlResponse
                {
                    Code = 200,
                    Command = "GetScore",
                    Message = ""
                };


            if (r.HasRows)
            {
                r.Read();
                s.Message = ((long) r["score"]).ToString(CultureInfo.InvariantCulture);

            }
            else
            {
                s.Message = "0";
            }

            _client.SendXmlResponse(s);


            db.Close();
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
                                "update HighScoreCommand with (serializable) set score = @score "+
                                "where userid = @userid and gameid = @gameid "+

                                "if @@rowcount = 0 "+
                                "begin "+
                                "    insert HighScoreCommand (userid, gameid, score) values (@userid, @gameid, @score) "+
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
