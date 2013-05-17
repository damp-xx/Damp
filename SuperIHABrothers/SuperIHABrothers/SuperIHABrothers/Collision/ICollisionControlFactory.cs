using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;

namespace Collision
{
    interface ICollisionControlFactory
    {
        CollisionControl GetCollisonControl(GameStateC gameState);
    }
}
