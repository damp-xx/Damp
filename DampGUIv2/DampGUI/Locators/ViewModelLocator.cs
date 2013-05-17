using CommunicationLibrary;

namespace DampGUI
{
    /// <summary>
    /// This class creates ViewModels on demand for Views, supplying a
    /// ServiceAgent to the ViewModel if required.
    /// <para>
    /// Place the ViewModelLocator in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocator xmlns:vm="clr-namespace:DampGUI"
    ///                                  x:Key="Locator" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// Use the <strong>mvvmlocator</strong> or <strong>mvvmlocatornosa</strong>
    /// code snippets to add ViewModels to this locator.
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static Games lGames = new Games();
        static Friends knowfriends = new Friends();
        // Create MainPageViewModel on demand
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                //ComLogin.Login("jens", "jens");

                LoadGames();
                LoadKnowFriend();
                ComEvents.EventSubscrie(new EventSubscriber());
                
                return new MainPageViewModel(lGames,knowfriends);
            }
        }

        public FriendFindViewModel FriendFindViewModel
        {
            get
            {
                return new FriendFindViewModel();
            }
        }

        public GameViewModel GameViewModel
        {
            get
            {
                return new GameViewModel(lGames);
            }
        }

        public FriendProfileViewModel FriendProfileViewModel
        {
            get
            {
                return new FriendProfileViewModel(knowfriends);
            }
        }

        public Games LoadGames()
        {
            ComGame game = new ComGame();
            game._games = lGames;
            game.GetAllGameList();
            lGames = game._games;
            return lGames;
        }

        public Friends LoadKnowFriend()
        {
            ComFriend friend = new ComFriend();
            friend._friends = knowfriends;
            friend.GetFriendList();
            knowfriends = friend._friends;
            return knowfriends;
        }
    }
}