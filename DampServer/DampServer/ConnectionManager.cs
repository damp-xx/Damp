#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using DampServer.interfaces;

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
                List<IConnection> tmpCons = new List<IConnection>();

                foreach (IConnection connection in _connections)
                {
                    if (!connection.UserHttp.IsConnected)
                        tmpCons.Add(connection);
                }

                foreach (IConnection connection in tmpCons)
                {
                    Console.WriteLine("User disconnected, removing user: {0}", connection.UserProfile.Username);
                    RemoveConnection(connection);

                    StatusXmlResponse xmlRes = new StatusXmlResponse
                        {
                            Code = 501,
                            Command = "UserWentOffline",
                        };

                    NotifyUserFriends(connection, xmlRes);
                }

                Thread.Sleep(1000);
            }
        }

        private void NotifyUserFriends(IConnection connection, StatusXmlResponse response)
        {
            foreach ( User user in connection.UserProfile.Friends)
            {
                IConnection con = GetConnectionByUserId(user.UserId);

                if (con != null && con.UserHttp != null)
                {
                    response.Message = con.UserProfile.UserId.ToString();
                    con.UserHttp.SendXmlResponse(response);
                }
            }
        }

        // @TODO MAKE THREAD SAFE!!
        public List<IConnection> GetOnlineUsers()
        {
            return _connections.ToList();
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

            StatusXmlResponse xmlRes = new StatusXmlResponse
            {
                Code = 601,
                Command = "UserWentOnline",
            };

            NotifyUserFriends(con, xmlRes);
            _connections.Add(con);
        }

        public IConnection GetConnectionByUserId(long userid)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.UserId == userid);
        }

        public IConnection GetConnectionByAuthToken(string authToken)
        {
            return _connections.FirstOrDefault(con => con.UserProfile.AuthToken == authToken);
        }
    }
}