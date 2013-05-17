using System;
using GameState;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprites
{
    internal class SpriteCD : ISprite
    {
        //Generel Attributes
        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private Vector2 _position;

        public Rectangle MyRectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public Vector2 Velocity { get; set; }
        public bool _isInAir { get; set; }
        public IAnchor _anchor;
        private Vector2 _AnchorOffset;
        private Texture2D _texture2D;
        private int _FrameHeight;
        private int _FrameWidth;
        private Rectangle _rectangle;
        private Rectangle _sourceRectangle;

        //Animation Attributes
        private float _timer; //Timer til at måle tiden som den aktuelle frame på animationern har været vist
        private float _interval = 200; //Interval som er tiden som hvert frame på animationen skal vises
        private int _currentFrame;


        public SpriteCD(Texture2D mTexture2D, Vector2 mAnchorOffset, int mFrameHeight, int mFrameWidth, IAnchor mAnchor)
        {
            _FrameHeight = mFrameHeight;
            _FrameWidth = mFrameWidth;
            _texture2D = mTexture2D;
            _AnchorOffset = mAnchorOffset;
            _anchor = mAnchor;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture2D, _rectangle, _sourceRectangle, Color.White);
        }

        public void Update(GameTime time)
        {
            Animate(time);

            _position.X = _anchor.Position.X + _AnchorOffset.X;
            _position.Y = _anchor.Position.Y + _AnchorOffset.Y;
            MyRectangle = new Rectangle((int) Position.X, (int) Position.Y, _FrameWidth, _FrameHeight);
            _sourceRectangle = new Rectangle( _FrameWidth*_currentFrame,0, _FrameWidth, _FrameHeight);
        }

        public void UpdatePosition()
        {
            MyRectangle = new Rectangle((int) Position.X, (int) Position.Y, _FrameWidth, _FrameHeight);
        }

        private void Animate(GameTime gameTime)
        {
            _timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds/2;
            if (_timer > _interval)
            {
                _currentFrame++;
                _timer = 0;
            }
            if (_currentFrame > 1 )
            {
                _currentFrame = 0;
            }
        }
    }
}