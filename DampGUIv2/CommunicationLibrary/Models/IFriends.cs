using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DampGUI
{
    public interface IFriends
    {
        void Add(IFriend aFriend);
        IFriend Get(int i);
        IFriend CurrentFriend { get; set; }
        int CurrentFriendIndex { get; set; }
        int TotalFriends { get; set; }
    }
}
