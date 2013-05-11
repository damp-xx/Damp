using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;
using Sprites;

namespace Collision
{
    class PlayerFinishlineEvent : IEvent
    {
        private IGameStateGameRunning _gameState;

        public PlayerFinishlineEvent(IGameStateGameRunning gameState)
        {
            _gameState = gameState;
            HandleEvent();
        }

        private void HandleEvent()
        {
            _gameState.IsGameRunning = false;
        }
    }
}
