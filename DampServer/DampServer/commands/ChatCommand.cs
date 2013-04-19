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

            User me = UserManagement.GetUserByAuthToken(http.Query.Get("authToken"));

            ChatXmlResponse r = new ChatXmlResponse
                {
                    Message = http.Query.Get("Message"),
                    To = http.Query.Get("To"),
                    From = me.Username,
                };

            //http.SendXmlResponse(r);

            if (receiver != null)
            {
                Console.WriteLine("ChatCommand: User online, sending chat");
                receiver.UserHttp.SendXmlResponse(r);
            }
            else
            {
                Console.WriteLine("ChatCommand: User not online, logging chat");
            }

            http.SendXmlResponse(new StatusXmlResponse
                {
                    Code = 200,
                    Message = http.Query.Get("Message"),
                    Command = "Chat"
                });
        }

        public bool NeedsAuthcatication { get; private set; }
        public bool IsPersistant { get; private set; }


        public List<XmlResponse> Notify(IUser user)
        {
            Database db = new Database();
            db.Open();

            var sqlCmd = db.GetCommand();

            sqlCmd.CommandText = "SELECT * FROM Chat WHERE \"receiver\" = @userid";
            sqlCmd.Parameters.Add("@userid", SqlDbType.BigInt).Value = user.UserId;

            SqlDataReader r = null;

            try
            {
                r = sqlCmd.ExecuteReader();
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(e.Message);
                return null;
            }

            List<XmlResponse> respons = new List<XmlResponse>();

            while (r.Read())
            {
                string from = ((long)r["sender"]).ToString();
                string message = (string)r["message"];
                string to = ((long) r["receiver"]).ToString();
                DateTime date = (DateTime) r["time"];

                respons.Add(new ChatXmlResponse
                {
                    From =  from,
                    Message = message,
                    To = to,
                    Date = date
                });  
            }

            db.Close();

            return respons;
        }
    }
}