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

        /**
         *  MainPageViewModel
         * 
         * @brief constructor that sets up the GUI, loads friend and games in 
         * @param IGames aGames, IFriends aFriends	 
         */
        public MainPageViewModel(IGames aGames, IFriends aFriends)
        {
            IndexFriend = 0;
            IndexGame = 0;
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

        /**
         *  ShowFriendSearch
         * 
         * @brief sets the FindFriendListView as the center usercontrol 
         */
        public void ShowFriendSearch()
        {
            Content = new FindFriendListView();
        }

        private object _content;

        /**
         *  Content
         * 
         * @brief property binded to the center usercontrol 
         * @param set(object)	 
         * @return get(object)
         */
        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                NotifyPropertyChanged(vm => vm.Content);
            }
        }
        /**
         *  IndexGame
         * 
         * @brief property to get game index 
         * @param set(int)	 
         * @return get(int)
         */
        public int IndexGame { get; set; }

        /**
         *  GotFocus
         * 
         * @brief function that is triggered when the game list has focus 
         */
        public void GotFocus()
        {
            FindIndex(IndexGame);
            ChangeGame();
        }

        private string gameListName;

        /**
          *  GameListName
          * 
          * @brief this is a property that is used to get the selected games name, and it is binded to the listview
          * @param set(string)
          * @return get(string)
          */
        public string GameListName
        {
            set
            {
                gameListName = value;
                FindIndex(IndexGame);
                OnPropertyChanged("GameListName");
            }
        }

        /**
         *  FindIndex
         * 
         * @brief this function is used to set the current game.
         * @param int aIndex
         */
        public void FindIndex(int aIndex)
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

        /**
         *  LGames
         * 
         * @brief this is a property that is connected to the listview
         * @param set(ObservableCollection<string>)
         * @return get(ObservableCollection<string>)
         */
        public ObservableCollection<string> LGames
        {
            get { return lGames; }
            set
            {
                lGames = value;
                NotifyPropertyChanged(vm => vm.LGames);
            }
        }


        /**
         *  FindGames
         * 
         * @brief this function search games and returns a list of all found games
         * @param string aName
         * @return List<string>
         */
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

        ////////////////Friends


        /**
         *  FindFriends
         * 
         * @brief this function search friends and returns a list of all found friends
         * @param string aName
         * @return List<IFriend>
         */
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

        /**
         *  LFriends
         * 
         * @brief this is a property that is connected to the listview
         * @param set(ObservableCollection<IFriend>)
         * @return get(ObservableCollection<IFriend>)
         */
        public ObservableCollection<IFriend> LFriends
        {
            get { return lFriends; }
            set
            {
                lFriends = value;
                NotifyPropertyChanged(vm => vm.LFriends);
            }
        }

        /**
 *  GotFocusF
 * 
 * @brief function that is triggered when the friend list has focus 
 */
        public void GotFocusF()
        {
            FindIndexFriend(IndexFriend);
            ChangeFriend();
        }

        /**
         *  IndexFriend
         * 
         * @brief property to get friend index 
         * @param set(int)	 
         * @return get(int)
         */
        public int IndexFriend { get; set; }

        private string _friendListName;
        /**
          *  FriendListName
          * 
          * @brief this is a property that is used to get the selected Friend name, and it is binded to the listview
          * @param set(IFriend)
          * @return get(IFriend)
          */
        public IFriend FriendListName
        {
            set
            {
                _friendListName = value.Name;
                FindIndexFriend(IndexFriend);
                OnPropertyChanged("FriendListName");
            }
        }

        /**
         *  FindIndex
         * 
         * @brief this function is used to set the current friend.
         * @param int aIndex
         */
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
        /**
        *  SearchGameName
        * 
        * @brief this property is used binded to the search game text
        * @param set(string)	 
        * @return get(string)
        */
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
        /**
        *  SearchFriendName
        * 
        * @brief this property is used binded to the search friend text
        * @param set(string)	 
        * @return get(string)
        */
        public string SearchFriendName
        {
            get { return _searchFriendName; }
            set
            {
                _searchFriendName = value;
                OnPropertyChanged("SearchFriendName");
            }
        }

        /**
         *  ChangeGame
         * 
         * @brief sets the GameControlView as the center usercontrol 
         */
        public void ChangeGame()
        {
            if (games.CurrentGame != null)
                Content = new GameControlView(games.CurrentGame.PhotoCollection);
        }
        /**
         *  SearchGames
         * 
         * @brief this function searches the games
         */
        public void SearchGames()
        {
            var foundGames = this.FindGames(SearchGameName);
            LGames = new ObservableCollection<string>(foundGames);
        }

        /**
         *  ChatButton
         * 
         * @brief this function starts up the ChatView with the selected friend
         */
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

        /**
         *  SearchFriends
         * 
         * @brief this function searches the friends
         */
        public void SearchFriends()
        {
            var foundFriends = this.FindFriends(SearchFriendName);
            LFriends = new ObservableCollection<IFriend>(foundFriends);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /**
         *  ChangeFriend
         * 
         * @brief sets the FriendProfileView as the center usercontrol 
         */
        public void ChangeFriend()
        {
            if (knowFriends.CurrentFriend != null)
                Content = new FriendProfileView();
        }

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