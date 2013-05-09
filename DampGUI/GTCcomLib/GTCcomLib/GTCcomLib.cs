using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTCcomLib
{
    public class GTCcomLib
    {
        private Process _gameClient = new Process();
        private AnonymousPipeServerStream _gameStreamOut = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
        private AnonymousPipeServerStream _gameStreamIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
        private StreamWriter _sw;
        private StreamReader _sr;
        private bool _stopGameTask;

        public GTCcomLib(string gamePath)
        {
            _gameClient.StartInfo.FileName = gamePath;
            _gameClient.StartInfo.Arguments = _gameStreamIn.GetClientHandleAsString() + " " +
                                              _gameStreamOut.GetClientHandleAsString();
            _gameClient.StartInfo.UseShellExecute = false;
            _gameClient.EnableRaisingEvents = true;
            _gameClient.Exited += new EventHandler(GameExited);

            _sw = new StreamWriter(_gameStreamOut);
            _sr = new StreamReader(_gameStreamIn);
        }

        public void RunGame()
        {
            _gameClient.Start();
            _gameStreamIn.DisposeLocalCopyOfClientHandle();
            _gameStreamOut.DisposeLocalCopyOfClientHandle();

            _stopGameTask = false;

            Task.Factory.StartNew(MessageTask);
        }

        public void StopGame()
        {
            _gameClient.Kill();
        }

        private void GameExited(object sender, System.EventArgs e)
        {
            _stopGameTask = true;
        }

        public bool SendMessage(string type)
        {
            try
            {
                using (var sw = new StreamWriter(_gameStreamOut))
                {
                    sw.AutoFlush = true;
                    sw.WriteLine(type +";");
                }
                return true;
            }
            catch (IOException e)
            {
                return false;
            }
        }
        
        public bool SendMessage(string type, string data)
        {
            try
            {
                using (_sw)
                {
                    _sw.AutoFlush = true;
                    _sw.WriteLine(type +";" +data);
                }
                return true;
            }
            catch (IOException e)
            {
                return false;
            }
        }

        private void MessageTask()
        {
            while (_stopGameTask != true)
            {
                HandleMessage(_sr.ReadLine());
            }
        }

        private bool HandleMessage(string message)
        {
            switch (message.Substring(0,3))
            {
                case "GHS":
                    return true;
                case "GPN":
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
