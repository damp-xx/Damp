using System.Collections.Generic;

namespace DampServer.interfaces
{
    public interface IUser
    {
        long UserId { get; set; }
        string Username { get; set; }
        List<Game> Games { get; set; }
        List<User> Friends { get; set; }
        string AuthToken { get; set; }
        string Email { get; set; }
    }
}