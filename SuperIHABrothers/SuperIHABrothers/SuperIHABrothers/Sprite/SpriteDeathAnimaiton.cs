///////////////////////////////////////////////////////////
//  SpriteDeathAnimaiton.cs
//  Implementation of the Class SpriteDeathAnimaiton
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:48:21
//  Original author: Space-Punk
///////////////////////////////////////////////////////////


using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
namespace Sprites {
	public class SpriteDeathAnimaiton : ISprite {

        //Generel Atributes
        public Vector2 Position { get; set; }
        private Vector2 _AnchorOffset;
        public Rectangle MyRectangle { get; set; }
	    public Vector2 Velocity { get; set; }
	    private IAnchor _Anchor;
        private int _FrameHeight;
        private int _FrameWidth;
        private Texture2D _texture2D;

        //Animation Atributes
        private int _interval = 75;
        private int _currentFrame;
        private float _timer = 0;

	    public void Draw(SpriteBatch spriteBatch)
	    {
	        throw new NotImplementedException();
	    }

	    public void Update(GameTime time)
	    {
	        throw new NotImplementedException();
	    }

	    public SpriteDeathAnimaiton(){
            throw new NotImplementedException();
		}


	}//end SpriteDeathAnimaiton

}//end namespace Sprites