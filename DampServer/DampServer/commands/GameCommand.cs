/**
 * @file   	GameCommand.cs
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file implements the server functions related to games
 * @section	LICENSE GPL 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DampServer.interfaces;
using DampServer.responses;

// @TODO VALIDATE DATA TYPE ON ALL SQL PARAMETARS!

namespace DampServer.commands
{
    class GameCommand : IServerCommand
    {
        private ICommandArgument _client;
        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
       
        public GameCommand()
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
                case "GetMyGames":
                    HandleMyGames();
                    break;
                case "GetAllGames":
                    HandleAllGames();
                    break;
                case "GameSearch":
                    HandleGameSearch();
                    break;
                case "BuyGame":
                    HandleBuyGame();
                    break;
                case "GameInfo":
                    HandleGameInfo();
                    break;
                case "GetGame":
                    HandleGetGame();
                    break;
            }
        }

        private void HandleBuyGame()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Id")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                {
                    Message = "Missing arguments"
                });

                return;
            }

            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT G.title as title, G.gameid as gameid, G.picture as picture," +
                " G.description  FROM Games G " +
                "WHERE gameid = @gameid";
            cmd.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("Id");

            SqlDataReader r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();

                Database db2 = new Database();
                db2.Open();

                SqlCommand cmd2 = db2.GetCommand();

                cmd2.CommandText = "INSERT INTO GameLibaray (gameid, userid) VALUES (@gameid, @userid)";
                cmd2.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("Id");
                cmd2.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
                
                cmd2.ExecuteNonQuery();

                db2.Close();


                _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "BuyGame",
                    Message = "Game Baught"
                });
            }
            else
            {
                _client.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 404,
                    Command = "BuyGame",
                    Message = "Can't find game"
                });
            }

            r.Close();
            db.Close();
        }

        private void HandleGetGame()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Id")))
            {
                _client.SendXmlResponse( new ErrorXmlResponse
                    {
                        Message = "Missing arguments"
                    });

                return;
            }

            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT G.title as title, G.gameid as gameid, G.picture as picture," +
                " G.description  FROM Games G INNER JOIN GameLibaray L ON G.gameid = L.gameid " +
                "WHERE L.userid = @userid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r = cmd.ExecuteReader();

            GameListResponse gl = new GameListResponse();
            gl.Games = new List<Game>();

            if (r.HasRows)
            {
                r.Read();

                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Command = "GetGame",
                        Code = 200,
                        Message = @"https://10.20.255.127:1337/GameDownload?authToken=" + _client.Query.Get("AuthToken") +
                                  "&Id=" + ((long) r["gameid"])
                    });
            }
            else
            {
                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Code = 404,
                        Command = "GetGame",
                        Message = "You don't have the game!"
                    });
            }

            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleGameInfo()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Id")))
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
                "SELECT G.title as title, G.gameid as gameid, G.picture as picture," +
                " G.description  FROM Games G WHERE gameid = @gameid";
            cmd.Parameters.Add("@gameid", SqlDbType.BigInt).Value = _client.Query.Get("Id");

            SqlDataReader r = cmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();
                Game g = new Game
                    {
                        Id = (long) r["gameid"],
                        Title = (string) r["title"],
                        Description = (string) r["description"],
     // @TODO FIX                   Picture = (string) r["picture"]
                    };

                _client.SendXmlResponse(g);
            }
            else
            {
                _client.SendXmlResponse(new StatusXmlResponse
                    {
                        Code = 404,
                        Command = "GameInfo",
                        Message = "Can't find game"
                    });
            }
      
        
            r.Close();
            db.Close();
        }

        private void HandleGameSearch()
        {
            if (string.IsNullOrEmpty(_client.Query.Get("Query")))
            {
                _client.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argurments!33!"
                    });
                
                return;
            }

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT G.title as title, G.gameid as gameid, G.picture as picture," +
                " G.description  FROM Games G WHERE title LIKE @query";
            cmd.Parameters.Add("@query", SqlDbType.NVarChar).Value = _client.Query.Get("Query") + "%";

            SqlDataReader r = cmd.ExecuteReader();

            GameListResponse gl = new GameListResponse();
            gl.Games = new List<Game>();

            if (r.HasRows)
                while (r.Read())
                {
                    gl.Games.Add(new Game
                    {
                        Id = (long)r["gameid"],
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                    // @TODO FIX    Picture = (string)r["picture"]
                    });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleAllGames()
        {
            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT title, gameid, langauge as language, mode,picture, description, genre, developer, recommendedage   FROM Games ";

            SqlDataReader r = cmd.ExecuteReader();

            GameListResponse gl = new GameListResponse();
            gl.Games = new List<Game>();

            if (r.HasRows)
                while (r.Read())
                {
                    var gg = new Game
                    {
                        Id = (long)r["gameid"],
                        Title = (string)r["title"],
                        Description = (string)r["description"],
                        Developer = (string) r["developer"],
                        Genre = (string) r["genre"],
                        RecommendedAge = (int)r["recommendedage"],
                        Language = (string) r["language"],
                        Mode = (string) r["mode"]
                    };

                    gg.Pictures = new List<string>();

                    var db4 = new Database();
                    db4.Open();
                    var sql4 = db4.GetCommand();

                    sql4.CommandText = "SELECT * FROM GamePictures WHERE gameid = @gameid";
                    sql4.Parameters.Add("@gameid", SqlDbType.BigInt).Value = gg.Id;

                    var rd = sql4.ExecuteReader();

                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            gg.Pictures.Add((string) rd["picture"]);
                        }
                    }

                    gl.Games.Add(gg);
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }

        private void HandleMyGames()
        {

            User user = UserManagement.GetUserByAuthToken(_client.Query.Get("AuthToken"));

            Database db = new Database();
            db.Open();
            SqlCommand cmd = db.GetCommand();

            cmd.CommandText =
                "SELECT G.title as title, G.gameid as gameid, G.picture as picture," +
                " G.description  FROM Games G INNER JOIN GameLibaray L ON G.gameid = L.gameid " +
                "WHERE L.userid = @userid";
            cmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r = cmd.ExecuteReader();

            GameListResponse gl = new GameListResponse();
            gl.Games = new List<Game>();

            if(r.HasRows)
                while (r.Read())
                {
                    gl.Games.Add(new Game
                        {
                            Id = (long) r["gameid"],
                            Title = (string) r["title"],
                            Description = (string)r["description"],
                        // @TODO FIX    Picture = (string) r["picture"]
                        });
                }
            r.Close();
            db.Close();

            _client.SendXmlResponse(gl);
        }
    }
}
