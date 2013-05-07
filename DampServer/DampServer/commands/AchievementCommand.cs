using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DampServer.interfaces;
using DampServer.responses;

namespace DampServer.commands
{
    public class AchievementCommand : IServerCommand
    {
        private ICommandArgument _client;
        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }

        public AchievementCommand()
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
                case "AddAchievement":
                    HandleAddAchievement();
                    break;
                case "GetAllMyAchievement":
                    HandleGetAllMyAchievement();
                    break;
                case "AchievementSearch":
                    HandleAchievementSearch();
                    break;
                case "GetAchievementsForGame":
                    HandleGetArhivementsForGame();
                    break;
                case "GetAllUserAchievements":
                    HandleGetAllUserArhivements();
                    break;
                case "GetUserAchievements":
                    HandleGetUserArhivements();
                    break;
                case "GetGameMyAchievements":
                    HandleGetGameMyAchievement();
                    break;
            }
        }

        private void HandleGetAllMyAchievement()
        {
            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " INNER JOIN ArcheivementIndex AI ON AI.archeivementid = A.archeivementid WHERE AI.userid = @userid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                        {
                            Title = (string) r["title"],
                            Description = (string)r["description"],
                            Icon = (string)r["picturePath"],
                            GameId = (long) r["gameid"],
                            ArcheivementId = (long)r["archeivementid"]
                        });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        // @TODO MAKE SO YOU CAN ONLY SEE FRIENDS ARCHIVEMENTS
        private void HandleGetUserArhivements()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("UserId")) || string.IsNullOrEmpty(_client.Query.Get("GameId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                {
                    Message = "Missing argurments!"
                });

                return;
            }


            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " INNER JOIN ArcheivementIndex AI ON AI.archeivementid = A.archeivementid WHERE AI.userid = @userid AND A.gameid = @gameid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = _client.Query.Get("UserId");
            cmd.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("GameId");



            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                    {
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Icon = (string)r["picturePath"],
                        GameId = (long)r["gameid"],
                        ArcheivementId = (long)r["archeivementid"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleGetAllUserArhivements()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("UserId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                {
                    Message = "Missing argurments!"
                });

                return;
            }


            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " INNER JOIN ArcheivementIndex AI ON AI.archeivementid = A.archeivementid WHERE AI.userid = @userid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = _client.Query.Get("UserId");


            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                    {
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Icon = (string)r["picturePath"],
                        GameId = (long)r["gameid"],
                        ArcheivementId = (long)r["archeivementid"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleGetArhivementsForGame()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("GameId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                {
                    Message = "Missing argurments!"
                });

                return;
            }


            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " WHERE A.gameid = @gameid";
            cmd.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("Gameid");


            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                    {
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Icon = (string)r["picturePath"],
                        GameId = (long)r["gameid"],
                        ArcheivementId = (long)r["archeivementid"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleAchievementSearch()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Query")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments"
                    });

                return;
            }

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " INNER JOIN ArcheivementIndex AI ON AI.archeivementid = A.archeivementid WHERE A.title = @query";
            cmd.Parameters.Add("@userid", SqlDbType.NVarChar).Value = _client.Query.Get("Query");

            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                    {
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Icon = (string)r["picturePath"],
                        GameId = (long)r["gameid"],
                        ArcheivementId = (long)r["archeivementid"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleGetGameMyAchievement()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("GameId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments!"
                    });

                return;
            }

            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT A.archeivementid as archeivementid, A.title as title, A.description as description," +
                " A.gameid as gameid, A.picturePath as picturePath FROM Archeivements A" +
                " INNER JOIN ArcheivementIndex AI ON AI.archeivementid = A.archeivementid WHERE AI.userid = @userid AND A.gameid = @gameid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            cmd.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("GameId");


            SqlDataReader r = cmd.ExecuteReader();

            AchievementListResponse gl = new AchievementListResponse();
            gl.Achievement = new List<Archivement>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Achievement.Add(new Archivement
                    {
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Icon = (string)r["picturePath"],
                        GameId = (long)r["gameid"],
                        ArcheivementId = (long)r["archeivementid"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleAddAchievement()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("ArchivementId")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments!"
                    });
                return;
            }

            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "INSERT INTO ArcheivementIndex (userid, archeivementid) Values(@userid, @aid)";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            cmd.Parameters.Add("@aid", SqlDbType.BigInt).Value = _client.Query.Get("ArchivementId");

            cmd.ExecuteNonQuery();

            _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "AddAchievement",
                    Message = "Achievement added"
                });
        }
    }
}
