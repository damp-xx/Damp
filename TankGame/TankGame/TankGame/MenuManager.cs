using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGame.Game_Screens;

namespace TankGame
{
    class MenuManager : IMenuManager
    {
        public IMenu chosenMenu { get; private set; }

        public void ShowMenu(IMenu nextMenu)
        {
            if (nextMenu != null)
                chosenMenu = nextMenu;
        }
    }
}
