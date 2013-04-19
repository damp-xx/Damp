#region

using System;
using System.Collections.Generic;
using DampServer.interfaces;

#endregion

namespace DampServer
{
    public class User : XmlResponse, IUser
    {
        public Int64 UserId { get; set; }
        public string Username { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Friends { get; set; }
        public string AuthToken { get; set; }
        public string Email { get; set; }
    }
}