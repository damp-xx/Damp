using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;
using Sprites;

namespace Collision
{
    public class PlayerFinishlineDetect : ICollisionDetect
    {
        private IGameStateGameRunning _gameRunning;

        public PlayerFinishlineDetect( IGameStateGameRunning gameRunning)
        {
            _gameRunning = gameRunning;
        }

        public void Detect(ISpriteContainerCollision mSpriteCollection, ISprite player, ISprite finishline)
        {
            if (player.MyRectangle.Right > (finishline.MyRectangle.Left + finishline.MyRectangle.Width/2))
            {
                new PlayerFinishlineEvent(_gameRunning);
            }
        }
    }
}