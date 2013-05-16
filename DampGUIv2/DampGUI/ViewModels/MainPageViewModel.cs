using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using SimpleMvvmToolkit;

namespace DampGUI
{
    public struct MainPageViewModelDispatcher
    {
        public IDispatcher MpvmDispatcher;
        public ObservableCollection<IFriend> friends;
    }

    /// <summary>
    ///     This class contains properties that a View can data bind to.
    /// </summary>
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;
        public Dispatcher dispatcher;
        public static MainPageViewModelDispatcher MpvmDispatcherclassDispatcher;
        private IGames games;
        private IFriends knowFriends;
        private ObservableCollection<string> allGames = new ObservableCollection<string>();
        private ObservableCollection<IFriend> allFriends = new ObservableCollection<IFriend>();

        public MainPageViewModel(IGames aGames, IFriends aFriends)
        {
            games = aGames;
            knowFriends = aFriends;
            MpvmDispatcherclassDispatcher = new MainPageViewModelDispatcher {MpvmDispatcher = Dispatcher, friends = LFriends};


            for (int i = 0; i < games.TotalGames; i++)
            {
                lGames.Add(games.Get(i).Title);
                allGames.Add(games.Get(i).Title);
            }

            for (int i = 0; i < knowFriends.TotalFriends; i++)
            {
                lFriends.Add(knowFriends.Get(i));
                allFriends.Add(knowFriends.Get(i));
            }
        }

        public void ShowFriendSearch()
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

        public void GotFocus()
        {
            findIndex(IndexGame);
            ChangeGame();
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
        public List<IFriend> FindFriends(string aName)
        {
            char[] name = aName.ToLower().ToCharArray();

            List<IFriend> resultList = new List<IFriend>();
            resultList.Clear();
            foreach (var friend in allFriends)
            {
                if (friend.Name.ToLower().IndexOf(aName.ToLower()) == 0)
                {
                    resultList.Add(friend);
                }
            }
            return resultList;
        }

        private ObservableCollection<IFriend> lFriends = new ObservableCollection<IFriend>();

        public ObservableCollection<IFriend> LFriends
        {
            get { return lFriends; }
            set
            {
                lFriends = value;
                NotifyPropertyChanged(vm => vm.LFriends);
            }
        }

        public void GotFocusF()
        {
            FindIndexFriend(IndexFriend);
            ChangeFriend();
        }

        private int _indexFriend = 0;

        public int IndexFriend
        {
            get { return _indexFriend; }
            set { _indexFriend = value; }
        }

        private string _friendListName;

        public IFriend FriendListName
        {
            set
            {
                _friendListName = value.Name;
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

        public void ChatButton()
        {
            if (knowFriends.CurrentFriend != null)
            {
                if (!EventSubscriber.ChatIdentyfier.ContainsKey(knowFriends.CurrentFriend.Id))
                {
                    var chat = new ChatView(knowFriends.CurrentFriend.Id);
                   // Console.WriteLine("id sendet::::::" + knowFriends.CurrentFriend.Id);

                    EventSubscriber.NewChat(knowFriends.CurrentFriend.Id, chat);
                    chat.Show();
                }
                else
                    EventSubscriber.ChatIdentyfier[knowFriends.CurrentFriend.Id].Focus();
            }
        }

        public void SearchFriends()
        {
            var foundFriends = this.FindFriends(SearchFriendName);
            LFriends = new ObservableCollection<IFriend>(foundFriends);
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