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
        public CollisionControl GetCollisonControl()
        {
            PlayerEnviromentDetect PE_Detect = new PlayerEnviromentDetect();
            PlayerMonsterDetect PM_Detect = new PlayerMonsterDetect();
            MonsterEnviromentDetect ME_Detect = new MonsterEnviromentDetect();
            
            
            
            /********************************' HAVE TO BE CHANGED!!!! **********************************/
            GameStateC gameState = new GameStateC();
            PlayerFinishlineDetect PF_Detect = new PlayerFinishlineDetect(gameState);






            CollisionControl returnElementCC = new CollisionControl(PE_Detect, PM_Detect, ME_Detect, PF_Detect);
            return returnElementCC;
        }
    }
}
