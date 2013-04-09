using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    interface IMap
    {
        List<Texture2D> TextureMap { get; }
        List<Vector2> PostionMap { get; }

        void Draw(SpriteBatch spriteBatch);

        void LoadContent(ContentManager content);

        void Initialize();
    }
}
