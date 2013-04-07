#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

// @TODO MAKE THREAD SAFE!!

namespace Damp
{
    public class ConnectionManager
    {
        private static readonly ConnectionManager Manager = new ConnectionManager();
        private readonly List<Connection> _connections = new List<Connection>();

        public ConnectionManager()
        {
            var newThread = new Thread(Run);
            newThread.Start();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    foreach ( var connection in _connections)
                    {
                        if (!connection.UserHttp.IsConnected)
                        {
                            Console.WriteLine("User disconnected, removing user: {0}", connection.UserProfile.Username);
                            RemoveConnection(connection);
                        }
                    }
                }
                catch
                {
                    // @TODO FIX THIS EXCEPTION!
                    // Console.WriteLine("Exception 1234: {0}", e.Message);
                }


                Thread.Sleep(1000);
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
            Logger.Log("User online");
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