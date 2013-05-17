using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientCommunication;
using Collision;
using GameState;
using Microsoft.Xna.Framework.Content;

namespace GameControle
{
    public class GameFactory : IGameFactory
    {
        
        public Game GetGame(ContentManager content, IPlayerDataGame mDataGame, IMessageConstructor messageConstructor)
        {
            var gameState = new GameStateC();
            var collisionControlFactory = new CollisionControlFactory();
            var collisionControl = collisionControlFactory.GetCollisonControl(gameState);
            var levelFactory = new FactoryLevel(content, collisionControl, gameState);     
            var keyboard = new KeybordInput();
            Game returnGame = new Game(levelFactory, gameState, keyboard, keyboard, content, mDataGame, messageConstructor);
            return returnGame;
        }
    }
}
