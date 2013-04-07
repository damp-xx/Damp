#region

using System.Net;
using System.Net.Sockets;

#endregion

namespace Damp
{
    public class SocketHandler
    {
        private readonly TcpListener _tcp = new TcpListener(IPAddress.Parse("0.0.0.0"), 1337);


        public SocketHandler()
        {
            _tcp.Start();

            while (true)
            {
                Socket s = _tcp.AcceptSocket();
                CommandProcessor commandProcessor = new CommandProcessor(s);
            }
// ReSharper disable FunctionNeverReturns
        }

// ReSharper restore FunctionNeverReturns
    }
}