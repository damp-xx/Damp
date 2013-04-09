#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

// @TODO MAKE THREAD SAFE!!

namespace DampServer
{
    public class ConnectionManager
    {
        public static  ConnectionManager Manager = new ConnectionManager();
        private readonly List<IConnection> _connections = new List<IConnection>();

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

        public void RemoveConnection(IConnection con)
        {
            _connections.Remove(con);
        }

        public void AddConnection(IConnection con)
        {
            Logger.Log("User online");
            _connections.Add(con);
        }

        public IConnection GetConnectionByUserId(int userid)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.UserId == userid);
        }

        public IConnection GetConnectionByAuthToken(string authToken)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.AuthToken == authToken);
        }
    }
}