using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.ObjectModel;
using CommunicationLibrary;
using SimpleMvvmToolkit;

namespace DampGUI
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// </summary>
    public class FriendFindViewModel : ViewModelBase<FriendFindViewModel>
    {
        private Friends allUsers;

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

        public void AddFriend()
        {
            if (FriendSearchList.CurrentFriend != null)
            {
                string msg = "Do you want to Add " + FriendSearchList.CurrentFriend.Name + "?";

                MessageBoxResult result = MessageBox.Show(msg, "Confirmation", MessageBoxButton.YesNo,
                                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ComFriend.AddFriend(FriendSearchList.CurrentFriend.Id);
                }
            }
        }

        public void SearchFriends()
        {
            var foundFriends = ComFriend.SearchUser(_name);


            FriendSearchList = foundFriends;
        }

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