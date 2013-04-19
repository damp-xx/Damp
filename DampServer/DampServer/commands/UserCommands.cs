#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using DampServer.interfaces;

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
          
            User user = new User();

     
           user.UserId = int.Parse(_http.Query.Get("UserId"));
          

            Database db = new Database();
            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE userid = @userid";
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            SqlDataReader r = sqlCmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();
                user.Username = (string) r["username"];
                user.Email = (string) r["email"];
            }
            else
            {
               Console.WriteLine("hewd");
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
                "SELECT * FROM Users WHERE userid = (SELECT userid1 FROM Friends WHERE userid = 1)";
            //    sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            r = sqlCmd.ExecuteReader();

            user.Friends = new List<User>();

            if(r.HasRows)
            while (r.Read())
            {
                
                User u = new User
                    {
                        Username = (string) r["username"],
                        Email = (string) r["email"]
                    };
                user.Friends.Add(u);  
            }
            
      
          
            r.Close();

            db.Close();

            _http.SendXmlResponse(user);
            
            
        }

        private void HandleGetMyUser()
        {
         

            User user = new User();

           
                user = UserManagement.GetUserByAuthToken(_http.Query.Get("authToken"));
      
            Database db = new Database();
            db.Open();

            SqlCommand sqlCmd = db.GetCommand();

            // get user
               // get user
            sqlCmd.CommandText = "SELECT TOP 1 * FROM Users WHERE userid = @userid";
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            SqlDataReader r = sqlCmd.ExecuteReader();

            if (r.HasRows)
            {
                r.Read();
                user.Username = (string)r["username"];
                user.Email = (string)r["email"];
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
                "SELECT * FROM Users WHERE userid = (SELECT userid1 FROM Friends WHERE userid = @userid)";
            //    sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;
            r = sqlCmd.ExecuteReader();

            user.Friends = new List<User>();
            if (r.HasRows)
            while (r.Read())
            {

                User u = new User { Username = (string)r["username"], Email = (string)r["email"] };
                user.Friends.Add(u);

            }
            r.Close();

            db.Close();

            _http.SendXmlResponse(user);               
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
            }
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }
    }
}