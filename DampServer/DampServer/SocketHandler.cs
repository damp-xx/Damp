#region

using System.Net;
using System.Net.Sockets;
using DampServer;

#endregion

namespace DampServer
{
    public class SocketHandler
    {
        private readonly TcpListener _tcp = new TcpListener(IPAddress.Parse("0.0.0.0"), 1337);


        public SocketHandler()
        {
            _tcp.Start();

            while (true)
            {
                var s = _tcp.AcceptTcpClient();
                new RequestProcessor(s);

            }
// ReSharper disable FunctionNeverReturns
        }

// ReSharper restore FunctionNeverReturns
    }
}