using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collision;
using GameState;
using Microsoft.Xna.Framework.Content;

namespace GameControle
{
    public class GameFactory : IGameFactory
    {
        public Game GetGame(ContentManager content)
        {
            var collisionControlFactory = new CollisionControlFactory();
            var collisionControl = collisionControlFactory.GetCollisonControl();
            var levelFactory = new FactoryLevel(content, collisionControl);
            var gameState = new GameStateC();
            var keyboard = new KeybordInput();

            Game returnGame = new Game(levelFactory, gameState, keyboard, keyboard);
            return returnGame;
        }
    }
}
