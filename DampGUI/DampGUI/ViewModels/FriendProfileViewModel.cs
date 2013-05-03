using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class FriendProfileViewModel : ViewModelBase<FriendProfileViewModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private IFriendProfileServiceAgent serviceAgent;
        Friends knowFriends;
        

        // Default ctor
        public FriendProfileViewModel() { }

        // TODO: ctor that accepts IXxxServiceAgent
        public FriendProfileViewModel(IFriendProfileServiceAgent serviceAgent, Friends aFriends)
        {
            this.serviceAgent = serviceAgent;
            knowFriends = aFriends;
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // TODO: Add properties using the mvvmprop code snippet
        public Friends KnowFriends
        {
            get { return knowFriends; }
            set
            {
                knowFriends = value;
            }
        }

        public Friend CurrentFriend
        {
            get
            {
                if (KnowFriends.CurrentFriendIndex >= 0)
                    return KnowFriends.CurrentFriend;
                else
                    return null;
            }
        }

        // TODO: Add methods that will be called by the view

        // TODO: Optionally add callback methods for async calls to the service agent
        
        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}