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
    
    public class BaseSprite
    {
        protected Texture2D myTexture;
        public Rectangle myRectangle { get; private set ; }
        protected Vector2 myPosition;


        public virtual void collition(BaseSprite objekt)
        {
                       
        }

        public virtual void update()
        {
            
        }

        public virtual void Draw()
        {
            
        }
    }
}
