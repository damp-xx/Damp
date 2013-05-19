using System;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// </summary>
    public class FriendProfileViewModel : ViewModelBase<FriendProfileViewModel>
    {
        /**
        *  FriendProfileViewModel
        * 
        * @brief this constructor sets IFriends up
        * @param IFriends aFriends	 
        */
        public FriendProfileViewModel(IFriends aFriends)
        {
            KnowFriends = aFriends;
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        /**
         *  KnowFriends
         * 
         * @brief this property is used by CurrentFriend property
         * @param set(IFriends)	 
         * @return get(IFriends)
         */
        public IFriends KnowFriends { get; set; }

        /**
         *  CurrentFriend
         * 
         * @brief this property gets the currentfriend, binded to all data
         * @param set(IFriend)	 
         * @return get(IFriend)
         */
        public IFriend CurrentFriend
        {
            get
            {
                if (KnowFriends.CurrentFriendIndex >= 0)
                {
                    return KnowFriends.CurrentFriend;
                }
                return null;
            }
        }
    }
}