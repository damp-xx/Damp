#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using DampServer.interfaces;
using DampServer.responses;

#endregion

namespace DampServer.commands
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

        private void HandleGetUser()
        {


           
            if (string.IsNullOrEmpty(_http.Query.Get("UserId")))
            {
                _http.SendXmlResponse(new ErrorXmlResponse { Message = "Invalid arguments #12" });
                return;
            }
          
            var user = GetUser(int.Parse(_http.Query.Get("UserId")));

            _http.SendXmlResponse(user);
            
            
        }

        private void HandleFriendSearch()
        {
            if (string.IsNullOrEmpty(_http.Query.Get("Query")))
            {
                _http.SendXmlResponse(new ErrorXmlResponse
                    {
                        Message = "Missing argument #129992"
                    });

                return;
            }

            Database db = new Database();

            db.Open();

            SqlCommand cmd = db.GetCommand();

            cmd.CommandText = "SELECT * FROM Users WHERE username LIKE @query";
            cmd.Parameters.Add("@query", SqlDbType.NVarChar).Value = _http.Query.Get("Query")+"%";

            SqlDataReader reader = cmd.ExecuteReader();

            FriendSearchResponse f = new FriendSearchResponse();
            f.Users = new List<User>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var  user = GetUser((long) reader["userid"]);
                    user.Friends = null;
                    f.Users.Add(user);
                }
            }

            _http.SendXmlResponse(f);

            db.Close();
        }

        private User GetUser(long id)
        {
            User user = UserManagement.GetUserById(id.ToString(CultureInfo.InvariantCulture));

            Database db = new Database();
            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            // get user
            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE userid = @userid";

            Console.WriteLine("SQL: {0}", user.UserId);

            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            SqlDataReader r = sqlCmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();
                user.Username = (string)r["username"];
                user.Email = (string)r["email"];
                if (!(r["description"] is DBNull))
                    user.Description = (string) r["description"];
                if (!(r["gender"] is DBNull))
                    user.Gender = (string)r["gender"];
                if (!(r["photo"] is DBNull))
                    user.Photo = (string)r["photo"];
                if (!(r["city"] is DBNull))
                    user.City = (string)r["city"];
                if (!(r["country"] is DBNull))
                    user.Country = (string)r["country"];
                if (!(r["language"] is DBNull))
                    user.Language = (string)r["language"];

            }
            r.Close();

            // get user games
            sqlCmd.CommandText =
                "SELECT * FROM Games WHERE gameid = (SELECT gameid FROM GameLibaray WHERE userid = @userid)";
            r = sqlCmd.ExecuteReader();

            user.Games = new List<Game>();
            if (r.HasRows)
                while (r.Read())
                {
                    Game g = new Game
                    {
                        Description = r["description"] as string,
                        Title = (string)r["title"],
                        Id = (long)r["gameid"]
                    };

                    user.Games.Add(g);
                }
            r.Close();

            // get user friends
            sqlCmd.CommandText =
                "SELECT u.* FROM Users u, Friends f WHERE f.userid = @userid AND u.userid = f.userid1";
            //    sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            r = sqlCmd.ExecuteReader();

            user.Friends = new List<User>();
            if (r.HasRows)
                while (r.Read())
                {

                    User u = new User
                        {
                            Username = (string) r["username"],
                            Email = (string) r["email"],
                            UserId = (long) r["userid"]
                        };

                    if (!(r["description"] is DBNull))
                        u.Description = (string)r["description"];
                    if (!(r["gender"] is DBNull))
                        u.Gender = (string)r["gender"];
                    if (!(r["photo"] is DBNull))
                        u.Photo = (string)r["photo"];
                    if (!(r["city"] is DBNull))
                        u.City = (string)r["city"];
                    if (!(r["country"] is DBNull))
                        u.Country = (string)r["country"];
                    if (!(r["language"] is DBNull))
                        u.Language = (string)r["language"];
                    
                    user.Friends.Add(u);

                }
            r.Close();

            db.Close();

            return user;
        }

        private User GetUser(string authtoken)
        {
            var user = UserManagement.GetUserByAuthToken(authtoken);
            return GetUser(user.UserId);
        }

        private void HandleGetMyUser()
        {
     
            _http.SendXmlResponse(GetUser(_http.Query.Get("AuthToken")));               
        }

        private void HandleForgotPassword()
        {
            if (string.IsNullOrEmpty(_http.Query.Get("Email")))
            {
                _http.SendXmlResponse(new ErrorXmlResponse {Message = "Missing argurments"});
                return;
            }


            Database db = new Database();
            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE email LIKE @email";
            sqlCmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = _http.Query.Get("Email");
            SqlDataReader r = sqlCmd.ExecuteReader();

           if (r.HasRows)
           {
               MailMessage mail = new MailMessage("you@damp.com", _http.Query.Get("Email"));
               SmtpClient client = new SmtpClient();
               client.Port = 25;
               client.DeliveryMethod = SmtpDeliveryMethod.Network;
               client.UseDefaultCredentials = false;
               client.Host = "smtp.iha.dk";
               mail.Subject = "this is a test email.";
               mail.Body = "this is my test email body";
               client.Send(mail);

               _http.SendXmlResponse(new StatusXmlResponse
                   {
                       Code = 200,
                       Command = "ForgottenPassword",
                       Message = "Email sent"
                   });
           }
           else
           {
               _http.SendXmlResponse(new StatusXmlResponse
               {
                   Code = 404,
                   Command = "ForgottenPassword",
                   Message = "Email not found"
               });
           }

            r.Close();            
        }

        public void Execute(ICommandArgument http, string cmd)
        {
            _http = http;

            switch (cmd)
            {
                case "GetMyUser":
                    HandleGetMyUser();
                    break;
                case "GetUser":
                    HandleGetUser();
                    break;
                case "ForgottenPassword":
                    HandleForgotPassword();
                    break;
                case "FriendSearch":
                    HandleFriendSearch();
                    break;
                case "AddUser":
                    HandleAddUser();
                    break;
            }
        }

        private void HandleAddUser()
        {
            if (string.IsNullOrEmpty(_http.Query.Get("FirstName")) || string.IsNullOrEmpty(_http.Query.Get("LastName")) ||
                string.IsNullOrEmpty(_http.Query.Get("Sex")) || string.IsNullOrEmpty(_http.Query.Get("Month")) ||
                string.IsNullOrEmpty(_http.Query.Get("Day")) || string.IsNullOrEmpty(_http.Query.Get("Year")) ||
                string.IsNullOrEmpty(_http.Query.Get("UserName")) || string.IsNullOrEmpty(_http.Query.Get("Password")) ||
                string.IsNullOrEmpty(_http.Query.Get("Email")))
            {
                
            }


            _http.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Command = "AddUser",
                    Message = "User added"
                });

        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}