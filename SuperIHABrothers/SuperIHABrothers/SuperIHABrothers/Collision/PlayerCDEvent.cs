using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;
using Sprites;

namespace Collision
{
    class PlayerCDEvent: IEvent
    {
        private ISpriteContainerCollision _containerCollision;
        private ISprite _CD;
        private IGameStateEvent _gameState;

        public PlayerCDEvent(ISpriteContainerCollision containerCollision, ISprite CD, IGameStateEvent gameState)
        {
            _containerCollision = containerCollision;
            _CD = CD;
            _gameState = gameState;
            HandleEvent();
        }

        private void HandleEvent()
        {
            _containerCollision.SpriteList[(int)listTypes.CD].Remove(_CD);
            _gameState.AddScore(1);
        }
    }
}
