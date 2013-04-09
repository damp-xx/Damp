using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TankGame.Game_Screens;

namespace TankGame
{
    interface IMenuManager
    {
        IMenu chosenMenu { get; }

        void ShowMenu(IMenu nextMenu);
    }
}
