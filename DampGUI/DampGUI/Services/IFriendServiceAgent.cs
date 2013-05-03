using System;
using System.Collections.Generic;

namespace DampGUI
{
    public interface IFriendServiceAgent
    {
        List<Friend> LoadFriends();
        List<Friend> FindFriends(string name, Friends aFriends);
    }
}
