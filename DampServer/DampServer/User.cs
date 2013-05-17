#region
using System.Collections.Generic;
using DampServer.interfaces;

#endregion

namespace DampServer
{
    public class User : XmlResponse, IUser
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public List<Game> Games { get; set; }
        public List<User> Friends { get; set; }
        public string AuthToken { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public string Gender { get; set; }
        public string Score { get; set; }
    }
}