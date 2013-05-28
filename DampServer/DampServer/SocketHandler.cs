#region

using System;
using System.Net;
using System.Net.Sockets;

#endregion

namespace DampServer
{
    public class SocketHandler
    {
        private readonly TcpListener _tcp = new TcpListener(IPAddress.Parse("0.0.0.0"), 1337);


        public SocketHandler()
        {
            try
            {
                _tcp.Start();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            while (true)
            {
                TcpClient s = _tcp.AcceptTcpClient();
// ReSharper disable ObjectCreationAsStatement
                new RequestProcessor(s);
// ReSharper restore ObjectCreationAsStatement
            }
// ReSharper disable FunctionNeverReturns
        }

// ReSharper restore FunctionNeverReturns
    }
}