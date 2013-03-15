#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace Damp
{
    /**
     * CommandProcessor
     */

    public class CommandProcessor
    {
        private static readonly List<IServerCommand> Commands = new List<IServerCommand>
            {
                new ChatCommand(),
                new DownloadCommand(),
                new LiveCommand(),
                new LoginCommand(),
                new UserCommands()
            };

        private readonly Socket _socket;

        public CommandProcessor(Socket s)
        {
            _socket = s;

            Thread newThread = new Thread(Run);
            newThread.Start();
        }

        public void AddCommand(IServerCommand cmd)
        {
            Commands.Add(cmd);
        }

        public void RemoveCommand(IServerCommand cmd)
        {
            Commands.Remove(cmd);
        }

        public void Run()
        {
            Console.WriteLine("Got Connection Proccesing it!");
            Http hp;

            try
            {
                hp = new Http(_socket);
            }
            catch (InvalidHttpRequestException e)
            {
                Console.WriteLine("Exception 2: Closing connection: {0}", e.Message);
                CloseSocket();
                return;
            }

            if (!string.IsNullOrEmpty(hp.Path))
            {
                Console.WriteLine("Looking for Command handler for {0}", hp.Path);

                IServerCommand serverCommand = Commands.FirstOrDefault(con => con.CanHandleCommand(hp.Path));

                if (serverCommand == null)
                {
                    ErrorXmlResponse r = new ErrorXmlResponse {Date = new DateTime(), Message = "Command not found!"};
                    hp.SendXmlResponse(r);
                    Console.WriteLine("Command not found!");
                }
                else
                {
                    Console.WriteLine("CommandProcesser.FoundCommand for {0}", hp.Path);

                    if (serverCommand.NeedsAuthcatication)
                    {
                        if (!CheckAuthetication(hp.Query.Get("AuthToken")))
                        {
                            ErrorXmlResponse r = new ErrorXmlResponse {Date = new DateTime(), Message = "Access Denied"};
                            hp.SendXmlResponse(r);

                            CloseSocket();
                            return;
                        }
                    }

                    try
                    {
                        serverCommand.HandleCommand(hp, hp.Path);
                    }
                    catch (InvalidHttpRequestException e)
                    {
                        ErrorXmlResponse r = new ErrorXmlResponse {Message = e.Message};
                        hp.SendXmlResponse(r);
                        CloseSocket();
                        return;
                    }


                    if (!serverCommand.IsPersistant) CloseSocket();
                    else
                    {
                        Console.WriteLine("Not Closing Connection is persistant!");
                        // _socket.EndDisconnect();
                    }
                }
            }
            else
            {
                Console.WriteLine("Request has no command!");
                ErrorXmlResponse r = new ErrorXmlResponse {Date = new DateTime(), Message = "Request has no command!"};
                hp.SendXmlResponse(r);
                CloseSocket();
            }
        }

        private void CloseSocket()
        {
            Console.WriteLine("Disconnect and close socket");
            //     _socket.Disconnect(false);
            _socket.Close();
        }

        private static bool CheckAuthetication(string authToken)
        {
            if (!string.IsNullOrEmpty(authToken))
                return true;
            return false;
        }
    }
}