using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class FriendFindViewModel : ViewModelBase<FriendFindViewModel>
    {
        // TODO: Add a member for IXxxServiceAgent
        private IFriendServiceAgent serviceAgent;
        private Friends allUsers;
        // Default ctor
        public FriendFindViewModel() { }

        // TODO: ctor that accepts IXxxServiceAgent
        public FriendFindViewModel(IFriendServiceAgent serviceAgent, Friends aAllUsers)
        {
            this.serviceAgent = serviceAgent;
            allUsers = aAllUsers;
        }

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        //private ObservableCollection<Friend> _friends =new ObservableCollection<Friend>();
        //public ObservableCollection<Friend> Friends
        //{
        //    get { return _friends; }
        //    set
        //    {
        //        _friends = value;
        //        NotifyPropertyChanged(vm => vm.Friends);
        //    }
        //}

        private ObservableCollection<string> _friendList = new ObservableCollection<string>(); 
        public ObservableCollection<string> FriendList
        {
            get
            {
                _friendList.Clear();
                for (int i = 0; i < _friendSearchList.Count; i++)
                {
                    _friendList.Add(_friendSearchList[i].Name);
                }
                return _friendList;
            }
           private set { }
        }

        private ObservableCollection<Friend> _friendSearchList = new ObservableCollection<Friend>();
        public ObservableCollection<Friend> FriendSearchList 
        {
            get
            {   
                return _friendSearchList;
            }
             set
            {
                _friendSearchList = value;
                NotifyPropertyChanged(vm => vm.FriendSearchList);
                NotifyPropertyChanged(vm => vm.FriendList);
            }
        }


        // TODO: Add properties using the mvvmprop code snippet

        // TODO: Add methods that will be called by the view

        public void LoadFriends()
        {
            ObservableCollection<Friend> result = new ObservableCollection<Friend>();
            for (int i = 0; i < allUsers.TotalFriends; i++)
            {
                result.Add(allUsers.Get(i));
            }
            
            FriendSearchList = new ObservableCollection<Friend>(result);
        }

        public void SearchFriends()
        {
            var foundFriends = serviceAgent.FindFriends(_name,allUsers);
            FriendSearchList = new ObservableCollection<Friend>(foundFriends);
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