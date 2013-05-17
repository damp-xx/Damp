using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;

namespace Sprite
{
    class SpriteScore : ISprite
    {
        public Vector2 Position { get; set; }
        public Rectangle MyRectangle { get; set; }
        public Vector2 Velocity { get; set; }
        public bool _isInAir { get; set; }
        private Vector2 _position;
        private SpriteFont _spriteFont;
        private string _scoreString;
        private IGameStateLevel _gameState;

        public SpriteScore(SpriteFont spriteFont, Vector2 position, IGameStateLevel gameState)
        {
            _spriteFont = spriteFont;
            _position = position;
            _gameState = gameState;
            _scoreString = "Score: ";
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_spriteFont, _scoreString, _position, Color.White);
        }
        
        public void Update(GameTime time)
        {

            _scoreString = "Score: " + _gameState.Score.ToString();
        }

        public void UpdatePosition()
        {
            throw new NotImplementedException();
        }
    }
}
