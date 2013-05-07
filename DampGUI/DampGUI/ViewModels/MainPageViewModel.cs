using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using SimpleMvvmToolkit;

// Toolkit namespace

namespace DampGUI
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    ///     </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        // Default ctor

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        private Games games;
        private Friends knowFriends;
        private ObservableCollection<string> allGames = new ObservableCollection<string>();
        private ObservableCollection<string> allFriends = new ObservableCollection<string>();

        // Add properties using the mvvmprop code snippet
        public MainPageViewModel(Games aGames, Friends aFriends)
        {
            games = aGames;
            knowFriends = aFriends;

            for (int i = 0; i < games.TotalGames; i++)
            {
                lGames.Add(games.Get(i).Title);
                allGames.Add(games.Get(i).Title);
            }

            for (int i = 0; i < knowFriends.TotalFriends; i++)
            {
                lFriends.Add(knowFriends.Get(i).Name);
                allFriends.Add(knowFriends.Get(i).Name);
            }
        }

        // TODO: Add methods that will be called by the view

        public void Button()
        {
            Content = new FindFriendListView();
        }

        private object _content;

        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged(vm => vm.Content);
            }
        }

        //games.CurrentIndex
        private int _indexGame = 0;

        public int IndexGame
        {
            get { return _indexGame; }
            set { _indexGame = value; }
        }

        private string gameListName;

        public string GameListName
        {
            set
            {
                gameListName = value;
                findIndex(_indexGame);
                OnPropertyChanged("GameListName");
            }
        }

        public void findIndex(int aIndex)
        {
            if (gameListName != null)
            {
                string name = gameListName;

                for (int i = 0; i < games.TotalGames; i++)
                {
                    if (name == games.Get(i).Title)
                    {
                        games.CurrentIndex = i;
                        NotifyPropertyChanged(vm => vm.games.CurrentIndex);
                        NotifyPropertyChanged(vm => vm.games.CurrentGame);
                        break;
                    }
                }
            }
        }

        private ObservableCollection<string> lGames = new ObservableCollection<string>();

        public ObservableCollection<string> LGames
        {
            get { return lGames; }
            set
            {
                lGames = value;
                NotifyPropertyChanged(vm => vm.LGames);
            }
        }


        public List<string> FindGames(string aName)
        {
            char[] name = aName.ToLower().ToCharArray();

            List<string> resultList = new List<string>();
            resultList.Clear();
            string strName;
            bool check = false;
            foreach (var game in allGames)
            {
                strName = game.ToLower();
                char[] GName = strName.ToCharArray();

                for (int i = 0; i <= (name.Length - 1); i++)
                {
                    if (name.Length <= GName.Length || !(name.Length > GName.Length))
                    {
                        if (name[i] == GName[i])
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (check || name.Length == 0)
                {
                    resultList.Add(game);
                    check = false;
                }
            }
            return resultList;
        }

        /////////////////////////////////Friends
        public List<string> FindFriends(string aName)
        {
            char[] name = aName.ToLower().ToCharArray();

            List<string> resultList = new List<string>();
            resultList.Clear();
            string strName;
            bool check = false;
            foreach (var friend in allFriends)
            {
                strName = friend.ToLower();
                char[] FName = strName.ToCharArray();

                for (int i = 0; i <= (name.Length - 1); i++)
                {
                    if (name.Length <= FName.Length || !(name.Length > FName.Length))
                    {
                        if (name[i] == FName[i])
                        {
                            check = true;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (check || name.Length == 0)
                {
                    resultList.Add(friend);
                    check = false;
                }
            }
            return resultList;
        }

        private ObservableCollection<string> lFriends = new ObservableCollection<string>();

        public ObservableCollection<string> LFriends
        {
            get { return lFriends; }
            set
            {
                lFriends = value;
                NotifyPropertyChanged(vm => vm.LFriends);
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

                for (int i = 0; i < knowFriends.TotalFriends; i++)
                {
                    if (name == knowFriends.Get(i).Name)
                    {
                        knowFriends.CurrentFriendIndex = i;
                        NotifyPropertyChanged(vm => vm.knowFriends.CurrentFriendIndex);
                        NotifyPropertyChanged(vm => vm.knowFriends.CurrentFriend);
                        break;
                    }
                }
            }
        }

        // TODO: Optionally add callback methods for async calls to the service agent

        private string _searchGameName;

        public string SearchGameName
        {
            get { return _searchGameName; }
            set
            {
                _searchGameName = value;
                OnPropertyChanged("SearchGameName");
            }
        }

        private string _searchFriendName;

        public string SearchFriendName
        {
            get { return _searchFriendName; }
            set
            {
                _searchFriendName = value;
                OnPropertyChanged("SearchFriendName");
            }
        }

        public void ChangeGame()
        {
            if (games.CurrentGame != null)
                Content = new GameControlView(games.CurrentGame.PhotoCollection);
        }

        public void SearchGames()
        {
            var foundGames = this.FindGames(SearchGameName);
            LGames = new ObservableCollection<string>(foundGames);
        }

        ///friends
        public void SearchFriends()
        {
            var foundFriends = this.FindFriends(SearchFriendName);
            LFriends = new ObservableCollection<string>(foundFriends);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ChangeFriend()
        {
            if (knowFriends.CurrentFriend != null)
                Content = new FriendProfileView();
        }

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