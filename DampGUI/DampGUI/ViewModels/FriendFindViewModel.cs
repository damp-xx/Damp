using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;
using CommunicationLibrary;
// Toolkit namespace
using System.Windows.Controls;
using System.Windows.Input;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class FriendFindViewModel : ViewModelBase<FriendFindViewModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private IFriendServiceAgent serviceAgent;
        private Friends allUsers;
        // Default ctor
        public FriendFindViewModel()
        {
        }

        // TODO: ctor that accepts IXxxServiceAgent
        public FriendFindViewModel(IFriendServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        private ObservableCollection<string> _friendList = new ObservableCollection<string>();

        public ObservableCollection<string> FriendList
        {
            get
            {
                _friendList.Clear();
                if (_friendSearchList != null)
                {
                    for (int i = 0; i < _friendSearchList.TotalFriends; i++)
                    {
                        _friendList.Add(_friendSearchList.Get(i).Name);
                    }
                }
                return _friendList;
            }
        }

        private Friends _friendSearchList = new Friends();

        public Friends FriendSearchList
        {
            get { return _friendSearchList; }
            set
            {
                _friendSearchList = value;
                NotifyPropertyChanged(vm => vm.FriendSearchList);
                NotifyPropertyChanged(vm => vm.FriendList);
            }
        }

        // TODO: Add properties using the mvvmprop code snippet

        private int _indexFriend = 0;

        public int IndexFriend
        {
            get { return _indexFriend; }
            set { _indexFriend = value; }
        }

        private string _friendListName;

        public string FriendListName
        {
            set
            {
                _friendListName = value;
                FindIndexFriend(_indexFriend);
                OnPropertyChanged("FriendListName");
            }
        }

        public void FindIndexFriend(int aIndex)
        {
            if (_friendListName != null)
            {
                string name = _friendListName;

                for (int i = 0; i < FriendSearchList.TotalFriends; i++)
                {
                    if (name == FriendSearchList.Get(i).Name)
                    {
                        FriendSearchList.CurrentFriendIndex = i;
                        NotifyPropertyChanged(vm => vm.FriendSearchList.CurrentFriendIndex);
                        NotifyPropertyChanged(vm => vm.FriendSearchList.CurrentFriend);
                        break;
                    }
                }
            }
        }

        // TODO: Add methods that will be called by the view

        public void AddFriend()
        {
            if (FriendSearchList.CurrentFriend != null)
            {
                string msg = "Do you want to Add " + FriendSearchList.CurrentFriend.Name + "?";
                MessageBoxResult result = MessageBox.Show(msg, "Confirmation", MessageBoxButton.YesNo,
                                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ComFriend.AddFriend(allUsers.CurrentFriend.Id);
                }
            }
        }

        public void SearchFriends()
        {
            var foundFriends = ComFriend.SearchUser(_name);
            FriendSearchList = foundFriends;
        }

        // TODO: Optionally add callback methods for async calls to the service agent

        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("SearchGameName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}