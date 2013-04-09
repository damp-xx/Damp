using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    class Map : IMap
    {
        public Map()
        { }

        public List<Texture2D> TextureMap { get; private set; }
        public List<Vector2> PostionMap { get; private set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < TextureMap.Count; i++)
            {
                spriteBatch.Draw(TextureMap[i],PostionMap[i],Color.White);
            }
        }

        public void LoadContent(ContentManager content)
        {
            TextureMap = new List<Texture2D>();
            PostionMap = new List<Vector2>();

            TextureMap.Add(content.Load<Texture2D>(@"MapBackground\1")); 
            PostionMap.Add(new Vector2(0, 0));
            TextureMap.Add(content.Load<Texture2D>(@"MapBackground\2"));
            PostionMap.Add(new Vector2(4000, 0));
            TextureMap.Add(content.Load<Texture2D>(@"MapBackground\3"));
            PostionMap.Add(new Vector2(0, 4000));
            TextureMap.Add(content.Load<Texture2D>(@"MapBackground\4"));
            PostionMap.Add(new Vector2(4000, 4000));            
        }

        public void Initialize()
        {
            
        }
    }
}
