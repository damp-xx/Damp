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
        static Friends allUsers = new Friends();
        // Create MainPageViewModel on demand
        public MainPageViewModel MainPageViewModel
        {
            get
            {
                LoadGames();
                LoadKnowFriend();
                LoadAllFriends();
                return new MainPageViewModel(lGames,knowfriends);
            }
        }

        public FriendFindViewModel FriendFindViewModel
        {
            get
            {
                IFriendServiceAgent serviceAgent = new MockFriendServiceAgent();
                
                return new FriendFindViewModel(serviceAgent,allUsers);
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

        public Games LoadGames()
        {
            ObservableCollection<string> Ach = new ObservableCollection<string>();
            Ach.Add("Shoot");
            Ach.Add("Boom");
            Ach.Add("Knife");
            Ach.Add("Rocket");
            string desc =
            @"THE NEXT INSTALLMENT OF THE WORLD'S # 1 ONLINE ACTION GAME Counter-Strike: Source blends Counter-Strike's award-winning teamplay action with the advanced technology of Source™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";

            PhotoCollection photoCollection1 = new PhotoCollection();
            photoCollection1.Path = @".\Game";

            PhotoCollection photoCollection2 = new PhotoCollection();
            photoCollection2.Path = @".\Game2"; 

            Game CS = new Game("CS", desc, "Action", "Damp", "Single", "English", Ach, photoCollection1);
            Game CSsovs = new Game("CSsovs", desc, "Action", "Damp", "Multi", "Danish", Ach, photoCollection2);

            lGames.Add(CS);
            lGames.Add(CSsovs);
            return lGames;
        }

        public Friends LoadKnowFriend()
        {
            ObservableCollection<string> Ach1 = new ObservableCollection<string>();
            Ach1.Add("Shoot");
            Ach1.Add("Boom");
            Ach1.Add("Knife");
            Ach1.Add("Rocket");


            ObservableCollection<string> Ach2 = new ObservableCollection<string>();
            Ach2.Add("Hunter");
            Ach2.Add("Lol");
            Ach2.Add("Dog");
            Ach2.Add("Cat");
 

            string desc1 =
            @"THE NEXT INSTALLMENT OF THE WORLD'S # 1 ONLINE ACTION GAME Counter-Strike: Source blends Counter-Strike's award-winning teamplay action with the advanced technology of Source™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";
            string desc2 =
            @"THE NEXT INSTAadasddsadadasdsadd sad asd sa dsa d saf    sa f  as d asd as fd asd as ds ad asd as d asd a ward-winning teamplay action with the advanced technology osadasd rce™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";

            Photo photo1 = new Photo(@".\ProfilePic\Ole_Profile.jpeg");
            Photo photo2 = new Photo(@".\ProfilePic\Poulina_Profile.jpeg");
            
            Friend Ole = new Friend("Ole", desc1, "Male", "Aarhus", "Denmark", "English", Ach1,photo1);
            Friend Poulina = new Friend("Poulina", desc2, "Female", "Minsk", "Poland", "Danish", Ach2,photo2);
            knowfriends.Add(Ole);
            knowfriends.Add(Poulina);
            return knowfriends;
        }

        public Friends LoadAllFriends()
        {
            ObservableCollection<string> Ach1 = new ObservableCollection<string>();
            Ach1.Add("Shoot");
            Ach1.Add("Boom");
            Ach1.Add("Knife");
            Ach1.Add("Rocket");


            ObservableCollection<string> Ach2 = new ObservableCollection<string>();
            Ach2.Add("Hunter");
            Ach2.Add("Lol");
            Ach2.Add("Dog");
            Ach2.Add("Cat");


            string desc1 =
            @"THE NEXT INSTALLMENT OF THE WORLD'S # 1 ONLINE ACTION GAME Counter-Strike: Source blends Counter-Strike's award-winning teamplay action with the advanced technology of Source™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";
            string desc2 =
            @"THE NEXT INSTAadasddsadadasdsadd sad asd sa dsa d saf    sa f  as d asd as fd asd as ds ad asd as d asd a ward-winning teamplay action with the advanced technology osadasd rce™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";

            Photo photo1 = new Photo(@"F:\Dropbox\DampGUI\DampGUI\bin\Debug\ProfilePic\Ole_Profile.jpeg");
            Photo photo2 = new Photo(@"F:\Dropbox\DampGUI\DampGUI\bin\Debug\ProfilePic\Poulina_Profile.jpeg");

            Friend Ole = new Friend("Ole", desc1, "Male", "Aarhus", "Denmark", "English", Ach1, photo1);
            Friend Poulina = new Friend("Poulina", desc2, "Female", "Minsk", "Poland", "Danish", Ach2, photo2);
            allUsers.Add(Ole);
            allUsers.Add(Poulina);
            return allUsers;
        }
    }
}