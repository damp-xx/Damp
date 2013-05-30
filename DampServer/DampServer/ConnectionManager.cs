/**
 * @file   	ConnectionManager.cpp
 * @author 	Bardur Simonsen, 11841
 * @date   	April, 2013
 * @brief  	This file implements the  ConnectionManager for DAMP Server
 * @section	LICENSE GPL 
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using DampServer.commands;
using DampServer.interfaces;
using DampServer.responses;

namespace DampServer
{
    /**
     * ConnectionManager
     * 
     * @brief Yay
     */

    public class ConnectionManager
    {
        public static volatile ConnectionManager Manager = null;

        private static readonly object SyncRoot = new Object();

        private readonly ConcurrentDictionary<int, IConnection> _connections =
            new ConcurrentDictionary<int, IConnection>();

        /**
         * ConnectionManager
         * 
         * @brief Default Constructor
         */

        public ConnectionManager()
        {
            Thread newThread = new Thread(Run);
            newThread.Start();
        }

        /**
         * Run
         * 
         * @brief connection managers run function
         * 
         * This functions runs infinately and checks if users are logging of
         */

        public void Run()
        {
            while (true)
            {
                ICollection<IConnection> tmpList = _connections.Values;


                tmpList.Where(x => !x.UserHttp.IsConnected).AsParallel().ForAll(connection =>
                    {
                        Console.WriteLine("User disconnected, removing user: {0}", connection.UserProfile.Username);
                        RemoveConnection(connection);

                        StatusXmlResponse xmlRes = new StatusXmlResponse
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

        /**
         * NotifyUserFirneds
         * 
         * @brief sends a StatusXmlResponse to all of the users friends
         * @param IConnection connection the user whos friends needs to be notified
         * @param StatuXmlResponse the notification message
         */

        public void NotifyUserFriends(IConnection connection, StatusXmlResponse response)
        {
            long userid = connection.UserProfile.UserId;
            connection.UserProfile.Friends.Select(user => GetConnectionByUserId(user.UserId))
                      .Where(con => con != null)
                      .AsParallel()
                      .ForAll(
                          con =>
                              {
                                  response.Message = userid.ToString(CultureInfo.InvariantCulture);
                                  con.UserHttp.SendXmlResponse(response);
                              });
        }

        /**
         * GetOnlineUsers
         * 
         * @brief return a clones list of the online users (connections)
         * @param List<IConnection> a cloned list of online connections
         */

        public List<IConnection> GetOnlineUsers()
        {
            List<IConnection> tmpList = _connections.Values.ToList();

            return tmpList;
        }

        /**
         * ConnectionManager
         * 
         * @brief returns a reference to a instance of the connectionmanager
         * @param Connectionmanger singleton connectionmanager
         */

        public static ConnectionManager GetConnectionManager()
        {
            if (Manager == null)
            {
                lock (SyncRoot)
                {
                    Manager = new ConnectionManager();
                }
            }
            return Manager;
        }

        /**
         * RemoveConnection
         * 
         * @brief Removes a connection from the connection manager
         * @param Iconnection con the user connection
         */

        public void RemoveConnection(IConnection con)
        {
            IConnection c;
            if (!_connections.TryRemove(con.GetHashCode(), out c))
            {
                Console.WriteLine("Error 9982, Can't remove con");
            }
        }

        /**
         * AddConnection
         * 
         * @brief Add a connection to the connection manager
         * @param IConnection con user connection
         */

        public void AddConnection(IConnection con)
        {
            Logger.Log("User online");

            StatusXmlResponse xmlRes = new StatusXmlResponse
                {
                    Code = 601,
                    Command = "UserWentOnline",
                };

            NotifyUserFriends(con, xmlRes);

            FriendCommand fc = new FriendCommand();
            foreach (XmlResponse d in       fc.Notify(con.UserProfile))
            {
                con.UserHttp.SendXmlResponse(d);
            }

            if (!_connections.TryAdd(con.GetHashCode(), con))
            {
                Console.WriteLine("Error 3211, Can't add connection to cdict");
            }
        }

        /**
         * GetConnectionByUserId
         * 
         * @brief Get a connection by user id
         * @param long userid the user id
         * @return IConnection the user connection, null if user not found
         */

        public IConnection GetConnectionByUserId(long userid)
        {
            return _connections.Values.FirstOrDefault(con => con.UserProfile.UserId == userid);
        }

        /**
         * GetConnectionByAuthToken
         * 
         * @brief Get a connection by user authentication token
         * @param string authToken users authentication token
         * @return IConnection the user connection, null if user not found
         */

        public IConnection GetConnectionByAuthToken(string authToken)
        {
            return _connections.Values.FirstOrDefault(con => con.UserProfile.AuthToken == authToken);
        }
    }
}