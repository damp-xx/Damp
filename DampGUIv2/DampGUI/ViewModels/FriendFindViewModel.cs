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
        
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        private ObservableCollection<string> _friendList = new ObservableCollection<string>();

        /**
         *  FriendList
         * 
         * @brief  this is a property that is connected to the listview
         * @param set(ObservableCollection<string>)
         * @return get(ObservableCollection<string>)
         */
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
        /**
         *  FriendSearchList
         * 
         * @brief  this is a property that is used by the FriendList Property 
         * @param set(Friends)
         * @return get(Friends)
         */
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

        /**
         *  IndexFriend
         * 
         * @brief  this is a property that is used to indicate the index of the list 
         * @param set(int)
         * @return get(int)
         */
        public int IndexFriend
        {
            get { return _indexFriend; }
            set { _indexFriend = value; }
        }

        private string _friendListName;

        /**
         *  FriendListName
         * 
         * @brief  this is a property that is used to get the selected friends name, and it is binded to the listview
         * @param set(string)
         * @return get(string)
         */
        public string FriendListName
        {
            set
            {
                _friendListName = value;
                FindIndexFriend(_indexFriend);
                OnPropertyChanged("FriendListName");
            }
        }

        /**
         *  FindIndexFriend
         * 
         * @brief this function is used to set the current friend.
         * @param int aIndex
         */
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

        /**
         *  AddFriend
         * 
         * @brief this function is binded to a button click event, and adds a friend to your friend list
         * @param 
         * @return
         */
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

        /**
         *  SearchFriends
         * 
         * @brief this function is binded to a textchanged event, and is used to find users
         */
        public void SearchFriends()
        {
            var foundFriends = ComFriend.SearchUser(Name);
            FriendSearchList = foundFriends;
        }

        /**
        *  Name
        * 
        * @brief this property is used binded to the search text
        * @param set(string)	 
        * @return get(string)
        */
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        /**
         *  OnPropertyChanged
         * 
         * @brief this function creates an event when a property is changed
         * @param string propertyName	 
         */
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}