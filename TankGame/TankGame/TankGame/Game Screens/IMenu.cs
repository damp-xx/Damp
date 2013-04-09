using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame.Game_Screens
{
    interface IMenu
    {
        void Draw(SpriteBatch spriteBatch);

        void Update();

        void Initialize();
    }
}
