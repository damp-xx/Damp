using System.Collections.Generic;
using System.Threading;
using DampServer.commands;
using DampServer.interfaces;

namespace DampServer
{
    class Notifier
    {
        private List<INotify> commands = new List<INotify>
            {
                new ChatCommand()
            };

        public Notifier()
        {
            var newThread = new Thread(Run);
            newThread.Start();
        }

        public void Run()
        {
            ConnectionManager connectionManager = ConnectionManager.GetConnectionManager();

            while (true)
            {
                foreach (INotify command in commands)
                {
                    foreach (IConnection onlineUser in connectionManager.GetOnlineUsers())
                    {
                        foreach (XmlResponse response in command.Notify(onlineUser.UserProfile))
                        {
                            onlineUser.UserHttp.SendXmlResponse(response);
                        }
                    }
                }

                Thread.Sleep(1000);
            }

        }
    }
}
