using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SuperIHABrothers
{
    class SpritePlayer: ISprite
    {
        private Vector2 _origin;
        private Vector2 _velocity;
        public Vector2 MyPosition { get; set; }
        private Texture2D myTexture;
        public Rectangle MyRectangle { get; private set; }
        private float _mass = 1;
        private bool _isInAir = true;
        private float _jumpPower = 5;
        private float _gravety = 0.2f;
        private float _timer;
        private float _interval = 75;
        

        private int _currentFrame;
        private int _frameHeight;
        private int _frameWidth;
        public SpritePlayer(Texture2D mTexture2D, Vector2 mPosition, int mFrameHeight, int mFrameWidth)
        {
            myTexture = mTexture2D;
            MyPosition = mPosition;
            _frameWidth = mFrameWidth;
            _frameHeight = mFrameHeight;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(myTexture, MyPosition, MyRectangle, Color.White, 0f, _origin, 1f, SpriteEffects.None, 0);
        }

        public void Update(GameTime gameTime)
        {
            MyRectangle = new Rectangle(_currentFrame*_frameWidth, 0, _frameWidth, _frameHeight);
            _origin = new Vector2(MyRectangle.Width/2, MyRectangle.Height/2);
            MyPosition += _velocity;

            if (_isInAir)
            {
                _velocity.Y += _gravety;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                AnimateRight(gameTime);
                
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                AnimateLeft(gameTime);
                
            }
            else _velocity.X = 0;

            if (Keyboard.GetState().IsKeyDown(Keys.Space) && _isInAir == false)
            {
                _velocity.Y = -_jumpPower;
                _isInAir = true;
            }
        }
        private void AnimateLeft(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (_timer > _interval)
            {
                _currentFrame++;
                _timer = 0;
                if (_currentFrame > 7||_currentFrame<4 ||_isInAir)
                {
                    _currentFrame = 4;
                }
            }
        }

        private void AnimateRight(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (_timer > _interval)
            {
                _currentFrame++;
                _timer = 0;
                if (_currentFrame > 3 || _isInAir)
                {
                    _currentFrame = 0;
                }
            }
        }
    }
}
