#region

using System;

#endregion

namespace DampServer
{
    public class ChatCommand : IServerCommand
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
            Connection receiver = null;
            try
            {
                receiver =
                    ConnectionManager.GetConnectionManager().GetConnectionByUserId(int.Parse(http.Query.Get("To")));
            }
            catch (Exception e)
            {
                Console.WriteLine("ChatCommandP Exception 211: {0}", e.Message);
            }

            User me = UserManagement.GetUser(http.Query.Get("authToken"));

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
    }
}