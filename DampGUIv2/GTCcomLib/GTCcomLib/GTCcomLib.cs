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

        public GTCcomLib(string gamePath)
        {
            _gameStreamIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
            _gameStreamOut = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            _gameClient.StartInfo.FileName = gamePath;
            _gameClient.StartInfo.Arguments = _gameStreamOut.GetClientHandleAsString() + " " +
                                              _gameStreamIn.GetClientHandleAsString();
            _gameClient.StartInfo.UseShellExecute = false;
            _gameClient.EnableRaisingEvents = true;
            _gameClient.Exited += new EventHandler(GameExited);   
        }

        public void RunGame()
        {
            _stopGameTask = false;
            _messageThread = new Thread(MessageTask);
            _gameClient.Start();
            _messageThread.Start();
            _gameStreamIn.DisposeLocalCopyOfClientHandle();
            _gameStreamOut.DisposeLocalCopyOfClientHandle();
        }

        public void StopGame()
        {
            _gameClient.Kill();
        }

        private void GameExited(object sender, EventArgs e)
        {
            _stopGameTask = true;
            _gameStreamIn.Close();
            _gameStreamOut.Close();
        }

        public bool SendMessage(string type, string data = "")
        {
            var sw = new StreamWriter(_gameStreamOut);
            sw.AutoFlush = true;
            try
            {
              sw.WriteLine(type + ":" + data);
                return true;
            }
            catch (IOException e)
            {
                return false;
            }
        }

        private void MessageTask()
        {
            var sr = new StreamReader(_gameStreamIn);
            while (_stopGameTask != true)
            {
                string message = sr.ReadLine();
                if(message != null)
                    HandleMessage(message);
            }
        }

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
