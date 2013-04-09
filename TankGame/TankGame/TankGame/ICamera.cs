using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    interface ICamera
    {
        Matrix transform { get; }

        void Update(Rectangle rectangle, Vector2 position);
    }
}
