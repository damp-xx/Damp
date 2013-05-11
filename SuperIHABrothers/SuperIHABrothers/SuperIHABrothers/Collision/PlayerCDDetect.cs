using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;
using Sprites;

namespace Collision
{
    class PlayerCDDetect: ICollisionDetect
    {
        private IGameStateEvent _gameState;

        public PlayerCDDetect( IGameStateEvent gameState)
        {
            _gameState = gameState;
        }

        public void Detect(ISpriteContainerCollision mSpriteCollection, ISprite player, ISprite coin)
        {
            new PlayerCDEvent(mSpriteCollection, coin, _gameState);
        }
    }
}
