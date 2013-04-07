#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;

#endregion

namespace Damp
{
    public class ConnectionManager
    {
        private static readonly ConnectionManager Manager = new ConnectionManager();
        private readonly List<Connection> _connections = new List<Connection>();

        public ConnectionManager()
        {
            Thread newThread = new Thread(Run);
            newThread.Start();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    foreach (
                        Connection connection in
                            _connections.Where(connection => !IsConnected(connection.UserHttp.Socket)))
                    {
                        Console.WriteLine("User disconnected, removing user: {0}", connection.UserProfile.Username);
                        RemoveConnection(connection);
                    }
                }
                catch (Exception e)
                {
                    // @TODO FIX THIS EXCEPTION!
                    Console.WriteLine("Exception 1234: {0}", e.Message);
                }


                Thread.Sleep(1000);
            }
        }

        public static bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public static ConnectionManager GetConnectionManager()
        {
            return Manager;
        }

        public void RemoveConnection(Connection con)
        {
            _connections.Remove(con);
        }

        public void AddConnection(Connection con)
        {
            _connections.Add(con);
        }

        public Connection GetConnectionByUserId(int userid)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.UserId == userid);
        }

        public Connection GetConnectionByAuthToken(string authToken)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.AuthToken == authToken);
        }
    }
}