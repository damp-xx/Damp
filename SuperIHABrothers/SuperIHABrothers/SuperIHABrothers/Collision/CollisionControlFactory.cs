using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collision;
using GameState;

namespace Collision
{
    class CollisionControlFactory : ICollisionControlFactory
    {
        public CollisionControl GetCollisonControl( GameStateC gameState)
        {
            PlayerEnviromentDetect PE_Detect = new PlayerEnviromentDetect();
            PlayerMonsterDetect PM_Detect = new PlayerMonsterDetect();
            MonsterEnviromentDetect ME_Detect = new MonsterEnviromentDetect();
            PlayerFinishlineDetect PF_Detect = new PlayerFinishlineDetect(gameState);
            PlayerCDDetect PC_Detect = new PlayerCDDetect(gameState);

            CollisionControl returnElementCC = new CollisionControl(PE_Detect, PM_Detect, ME_Detect, PF_Detect, PC_Detect);
            return returnElementCC;
        }
    }
}
