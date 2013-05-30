using System.Collections.Generic;
using System.Threading;
using DampServer.commands;
using DampServer.interfaces;

namespace DampServer
{
    internal class Notifier
    {
        private readonly List<INotify> _commands = new List<INotify>
            {
                new ChatCommand()
            };

        public Notifier()
        {
            Thread newThread = new Thread(Run);
            newThread.Start();
        }

        public void Run()
        {
            ConnectionManager connectionManager = ConnectionManager.GetConnectionManager();

            while (true)
            {
                foreach (INotify command in _commands)
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

// ReSharper disable FunctionNeverReturns
        }

// ReSharper restore FunctionNeverReturns
    }
}