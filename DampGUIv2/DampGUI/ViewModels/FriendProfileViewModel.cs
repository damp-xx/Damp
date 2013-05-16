using System;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// </summary>
    public class FriendProfileViewModel : ViewModelBase<FriendProfileViewModel>
    {
        public FriendProfileViewModel(IFriends aFriends)
        {
            KnowFriends = aFriends;
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        public IFriends KnowFriends { get; set; }

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

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}