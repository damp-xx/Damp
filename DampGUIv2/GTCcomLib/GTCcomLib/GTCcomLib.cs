/**
 * @file   	GTCcomLib.cs
 * @author 	Pierre-Emil Zachariasen, 11833
 * @date   	may, 2013
 * @brief  	This file implements the GTCcomLib class, for the client to open games and communicate with them
 * @section	LICENSE GPL 
 */
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using CommunicationLibrary;

namespace GTCcomLib
{
    public class GTCcomLib
    {
        private Process _gameClient = new Process();
        private AnonymousPipeServerStream _gameStreamOut = null;
        private AnonymousPipeServerStream _gameStreamIn = null;
        private Thread _messageThread;
        private bool _stopGameTask;

        /**
         * @Brief Constructor for GTCcomLib
         * @Param String gamePath - path to the game executable file relative to the application
         */
        public GTCcomLib(string gamePath)
        {
            _gameStreamIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
            _gameStreamOut = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            _gameClient.StartInfo.FileName = gamePath;
            _gameClient.StartInfo.Arguments = _gameStreamOut.GetClientHandleAsString() + " " +
                                              _gameStreamIn.GetClientHandleAsString();
            _gameClient.StartInfo.UseShellExecute = false;
            _gameClient.EnableRaisingEvents = true;
            _gameClient.Exited += GameExited;   
        }
        /**
         * @Brief Starts the game proces and a thread to incoming communication
         */
        public void RunGame()
        {
            _stopGameTask = false;
            _messageThread = new Thread(MessageThread);
            _gameClient.Start();
            _messageThread.Start();
            _gameStreamIn.DisposeLocalCopyOfClientHandle();
            _gameStreamOut.DisposeLocalCopyOfClientHandle();
        }
        /**
         * @Brief Stops the game
         */
        public void StopGame()
        {
            _gameClient.Kill();
        }
        /**
         * @Brief Eventhandlers for GameExited Event. Closes pipes and communication thread
         */
        private void GameExited(object sender, EventArgs e)
        {
            _stopGameTask = true;
            _gameStreamIn.Close();
            _gameStreamOut.Close();
        }
        /**
         * @Brief Method to send a message to the game
         * @Param string type - the command type to send, see protocol for accepted commands
         * @Param string data - data to the command
         */
        public bool SendMessage(string type, string data = "")
        {
            var sw = new StreamWriter(_gameStreamOut) {AutoFlush = true};
            try
            {
              sw.WriteLine(type + ":" + data);
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
        /**
         * @Brief Thread to recieve incoming messages
         */
        private void MessageThread()
        {
            var sr = new StreamReader(_gameStreamIn);
            while (_stopGameTask != true)
            {
                string message = sr.ReadLine();
                if(message != null)
                    HandleMessage(message);
            }
        }
        /**
         * @Brief Method to handle recieved messages
         * @Param String message - The recieved message to handle
         * @Return bool - returns true if the message was understood, otherwise returns false
         */
        private bool HandleMessage(string message)
        {
            switch (message.Substring(0,3))
            {
                case "GHS":
                    return true;
                case "GPN":
                    SendMessage("CPN",ComProfile.GetProfileName());
                    return true;
                case "ACH":
                    return true;
                case "NHS":
                    return true;
                default:
                    return false;
            }
        }
    }
}
