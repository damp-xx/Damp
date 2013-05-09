/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DampGUI"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using CommunicationLibrary;
// Toolkit namespace

using SimpleMvvmToolkit;

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
                ComLogin.Login("jens", "jens");
               
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
                IFriendServiceAgent serviceAgent = new MockFriendServiceAgent();
                
                return new FriendFindViewModel(serviceAgent);
            }
        }

        public GameViewModel GameViewModel
        {
            get
            {
                IGameServiceAgent serviceAgent = new MockGameServiceAgent();
                return new GameViewModel(serviceAgent,lGames);
            }
        }

        public FriendProfileViewModel FriendProfileViewModel
        {
            get
            {
                IFriendProfileServiceAgent serviceAgent = new MockFriendProfileServiceAgent();
                return new FriendProfileViewModel(serviceAgent, knowfriends);
            }
        }

        public ChatViewModel ChatViewModel
        {
            get
            {
                
                return new ChatViewModel(knowfriends.CurrentFriend.Id);
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