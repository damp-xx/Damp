#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using DampServer.interfaces;

#endregion

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
               
                List<IConnection> tmpList = _connections.ToList();

                tmpList.Where(x => !x.UserHttp.IsConnected).AsParallel().ForAll( connection =>
                    {
                        Console.WriteLine("User disconnected, removing user: {0}", connection.UserProfile.Username);
                        RemoveConnection(connection);

                        var xmlRes = new StatusXmlResponse
                        {
                            Code = 501,
                            Command = "UserWentOffline",
                        };

                        NotifyUserFriends(connection, xmlRes);
                    });

                Thread.Sleep(1000);
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns

        private void NotifyUserFriends(IConnection connection, StatusXmlResponse response)
        {
            connection.UserProfile.Friends.Select(user => GetConnectionByUserId(user.UserId))
                      .Where(con => con != null)
                      .AsParallel()
                      .ForAll(
                          con =>
                              {
                                  response.Message = con.UserProfile.UserId.ToString(CultureInfo.InvariantCulture);
                                  con.UserHttp.SendXmlResponse(response);
                              });
        }

        public List<IConnection> GetOnlineUsers()
        {
            return _connections.ToList();
        }

        public static ConnectionManager GetConnectionManager()
        {
            return Manager;
        }

        public void RemoveConnection(IConnection  con)
        {
            lock (_connections)
            {
                _connections.Remove(con);
            }
        }

        public void AddConnection(IConnection con)
        {
            Logger.Log("User online");

            var xmlRes = new StatusXmlResponse
            {
                Code = 601,
                Command = "UserWentOnline",
            };

            NotifyUserFriends(con, xmlRes);

            lock (_connections)
            {
                _connections.Add(con);
            }
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