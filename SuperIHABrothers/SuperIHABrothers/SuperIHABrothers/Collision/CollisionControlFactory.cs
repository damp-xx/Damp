using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collision;

namespace Collision
{
    class CollisionControlFactory : ICollisionControlFactory
    {
        public CollisionControl GetCollisonControl()
        {
            PlayerEnviromentDetect PE_Detect = new PlayerEnviromentDetect();
            PlayerMonsterDetect PM_Detect = new PlayerMonsterDetect();
            MonsterEnviromentDetect ME_Detect = new MonsterEnviromentDetect();

            CollisionControl returnElementCC = new CollisionControl(PE_Detect, PM_Detect, ME_Detect);
            return returnElementCC;
        }
    }
}
