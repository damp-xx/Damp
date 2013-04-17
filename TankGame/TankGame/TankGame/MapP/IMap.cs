using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame.MapP
{
    interface IMap
    {
        List<Texture2D> TextureMap { get; }
        List<Vector2> PostionMap { get; }
        List<IObstacle> Obstacles { get;  }

        void Draw(SpriteBatch spriteBatch);

        void LoadContent(ContentManager content);

        void Initialize();
    }
}
