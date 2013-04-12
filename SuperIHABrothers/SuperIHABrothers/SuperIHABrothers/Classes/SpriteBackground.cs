using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperIHABrothers.Interfaces;

namespace SuperIHABrothers.Classes
{
    public class SpriteBackground: ISprite

    {
        public Rectangle MyRectangle { get; private set; }
        public Vector2 MyPosition { get; set; }
        private Texture2D _texture2D;
        private Vector2 _position;
        private IAnchor _anchor;
        private int _FrameHeight;
        private int _FrameWidth;
        private int _Distance; //Distance from forground to background
        public SpriteBackground(Texture2D mTexture2D, Vector2 mPosition, IAnchor mAnchor,  int mFrameHeight, int mFrameWidth, int mDistance)
        {
            _texture2D = mTexture2D;
            _position = mPosition;
            _FrameHeight = mFrameHeight;
            _FrameWidth = mFrameWidth;
            _Distance = mDistance;
            _anchor = mAnchor;
        }
        public void Update(GameTime gameTime)
        {
            _position.Y = _anchor.Position.Y/_Distance;
            _position.X = _anchor.Position.X / _Distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
