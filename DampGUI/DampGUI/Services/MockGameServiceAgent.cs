using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMvvmToolkit;

namespace DampGUI
{
    class MockGameServiceAgent : IGameServiceAgent
    {
        Games lGames;

        public Games LoadGames()
        {
            ObservableCollection<string> Ach = new ObservableCollection<string>();
            Ach.Add("Shoot");
            Ach.Add("Boom");
            Ach.Add("Knife");
            Ach.Add("Rocket");
            string desc = 
            @"THE NEXT INSTALLMENT OF THE WORLD'S # 1 ONLINE ACTION GAME Counter-Strike: Source blends Counter-Strike's award-winning teamplay action with the advanced technology of Source™ technology. Featuring state of the art graphics, all new sounds, and introducing physics, Counter-Strike: Source is a must-have for every action gamer.";

            //Game CS = new Game("CS",desc,"Action","Damp","Single","English",Ach);
            //Game CSsovs = new Game("CSsovs",desc, "Action", "Damp", "Multi","Danish", Ach);

            //lGames.Add(CS);
            //lGames.Add(CSsovs);
            return lGames;
        }

    }
}
