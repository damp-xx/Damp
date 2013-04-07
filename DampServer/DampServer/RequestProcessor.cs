/**
 * @file   	RequestProcessor.cpp
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file implements the command processor for DAMP Server
 * @section	LICENSE GPL 
 */

#region

using System;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace DampServer
{
    /**
    * @brief RequestProcessor Constructor
    */
    public class RequestProcessor
    {
       
        private readonly TcpClient _socket;

        public RequestProcessor(TcpClient s)
        {
            _socket = s;

            var newThread = new Thread(Run);
            newThread.Start();
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

            if (string.IsNullOrEmpty(hp.Path))
            {
                Console.WriteLine("Request has no command!");
                var r = new ErrorXmlResponse {Date = new DateTime(), Message = "Request has no command!"};
                hp.SendXmlResponse(r);
                CloseSocket();
            }
       
            Console.WriteLine("Looking for Command handler for {0}", hp.Path);

            try
            {
                var serverCommand = CreateCommand(hp.Path);
                Console.WriteLine("CommandProcesser.FoundCommand for {0}", hp.Path);


                if (serverCommand.NeedsAuthcatication)
                {
                    if (!CheckAuthetication(hp.Query.Get("AuthToken")))
                    {
                        var r = new ErrorXmlResponse {Date = new DateTime(), Message = "Access Denied"};
                        hp.SendXmlResponse(r);

                        CloseSocket();
                        return;
                    }
                }

                serverCommand.Execute(hp, hp.Path);

                if (!serverCommand.IsPersistant) CloseSocket();
                else
                {
                    Console.WriteLine("Not Closing Connection is persistant!");
                }

            }
            catch (CommandNotFoundException e)
            {
                var r = new ErrorXmlResponse {Date = new DateTime(), Message = "Command not found! " + e.Message };
                hp.SendXmlResponse(r);
                CloseSocket();
                Console.WriteLine("Command not found!");
            }
            catch (InvalidHttpRequestException e)
            {
                var r = new ErrorXmlResponse {Message = e.Message};
                hp.SendXmlResponse(r);
                CloseSocket();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                hp.SendXmlResponse(new ErrorXmlResponse { Message = "Internal Server Error! #1337" });
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
            try
            {
                UserManagement.GetUser(authToken);
                return true;
            }
            catch (UserNotFoundException)
            {
                return false;
            }
        }

        /**
         * CreateCommand
         * 
         * @brief returns a command object to handle a given cmd
         * @param string cmd cmd to handle
         * @return IServerCommand object that handles the given command
         */
        public static IServerCommand CreateCommand(string cmd)
        {
            IServerCommand leCmd;
            switch (cmd)
            {
                case "Chat":
                    leCmd=new ChatCommand();
                    break;
                case "Login":
                    leCmd=new LoginCommand();
                    break;
                case "Live":
                    leCmd = new LiveCommand();
                    break;
                case "UploadGame":
                    leCmd = new UploadGameCommand();
                    break;
                case "GetUser":
                    leCmd = new UserCommands();
                    break;
                case "GetMyUser":
                    leCmd = new UserCommands();
                    break;
                default:
                    throw new CommandNotFoundException(cmd);
            }

            return leCmd;
        }
    }
}