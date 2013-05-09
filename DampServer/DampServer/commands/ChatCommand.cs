#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using DampServer.exceptions;
using DampServer.interfaces;

#endregion

namespace DampServer.commands
{
    public class ChatCommand : IServerCommand, INotify
    {
        public ChatCommand()
        {
            NeedsAuthcatication = true;
            IsPersistant = false;
        }

        public bool CanHandleCommand(string s)
        {
            return s.Equals("Chat");
        }

        public void Execute(ICommandArgument http, string cmd = null)
        {
            if (string.IsNullOrEmpty(http.Query.Get("To")) ||
                string.IsNullOrEmpty(http.Query.Get("Message")))
            {
                throw new InvalidHttpRequestException("Missing argurments!");
            }

            // @TODO needs verification
            IConnection receiver = null;
            try
            {
                receiver =
                    ConnectionManager.GetConnectionManager().GetConnectionByUserId(int.Parse(http.Query.Get("To")));
            }
            catch (Exception e)
            {
                Console.WriteLine("ChatCommandP Exception 211: {0}", e.Message);
            }

            User me = UserManagement.GetUserByAuthToken(http.Query.Get("AuthToken"));

            var r = new StatusXmlResponse
                {
                    Message = http.Query.Get("Message"),
                    To = http.Query.Get("To"),
                    From = me.Username,
                    Command = "ChatRecieved"
                };

            var db = new Database();
            db.Open();

            SqlCommand cmd2 = db.GetCommand();

            cmd2.CommandText =
                "INSERT INTO Chat (sender, receiver, message, seen, time) Values(@sender, @sender, @message, @seen, getdate())";
            cmd2.Parameters.Add("@sender", SqlDbType.BigInt).Value = me.UserId;
            cmd2.Parameters.Add("@receiver", SqlDbType.BigInt).Value = r.To;
            cmd2.Parameters.Add("@message", SqlDbType.Text).Value = r.Message;

            if (receiver != null)
            {
                cmd2.Parameters.Add("@seen", SqlDbType.TinyInt).Value = 1;
                Console.WriteLine("ChatCommand: User online, sending chat");
                receiver.UserHttp.SendXmlResponse(r);
            }
            else
            {
                cmd2.Parameters.Add("@seen", SqlDbType.TinyInt).Value = 0;
                Console.WriteLine("ChatCommand: User not online, logging chat");
            }

            cmd2.ExecuteNonQuery();

            http.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Message = http.Query.Get("Message"),
                    Command = "ChatSend"
                });
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }

        public List<XmlResponse> Notify(IUser user)
        {
            var db = new Database();
            db.Open();

            var sqlCmd = db.GetCommand();

            sqlCmd.CommandText = "SELECT * FROM Chat WHERE \"receiver\" = @userid AND seen = 0";
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r;

            try
            {
                r = sqlCmd.ExecuteReader();
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(e.Message);
                return null;
            }

            var respons = new List<XmlResponse>();

            while (r.Read())
            {
                var from = ((long)r["sender"]).ToString(CultureInfo.InvariantCulture);
                var message = (string)r["message"];
                string to = ((long) r["receiver"]).ToString(CultureInfo.InvariantCulture);
                var date = (DateTime) r["time"];

                respons.Add(new StatusXmlResponse
                {
                    From =  from,
                    Message = message,
                    To = to,
                    Date = date,
                    Command = "ChatReceived"
                   
                }); 
 
                var db2 = new Database();
                db2.Open();

                SqlCommand cmd3 = db2.GetCommand();

                cmd3.CommandText = "UPDATE Chat SET seen = 1 WHERE chatid = @id";
                cmd3.Parameters.Add("@id", SqlDbType.BigInt).Value = r["chatid"];
                cmd3.ExecuteNonQuery();
            }

            db.Close();

            return respons;
        }
    }
}