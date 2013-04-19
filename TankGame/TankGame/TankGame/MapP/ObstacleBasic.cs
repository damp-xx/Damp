using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame.MapP
{
    class ObstacleBasic : IObstacle
    {
        public Texture2D Texture { get; private set; }
        public Rectangle Rectangle { get; private set; }
        public Vector2 Position { get; private set; }
        public Vector2 Origin { get; private set; }
        public float Rotation { get; private set; }
        
        public ObstacleBasic(Texture2D texture, Vector2 position, float rotation)
        {
            Texture = texture;
            Position = position;
            Rotation = rotation;

           Initialize();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, Origin, 1f, SpriteEffects.None, 0);
        }

        public void Update()
        {
            // Not used in the basic version of Obstacle
        }

        public void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        private void Initialize()
        {
            Rectangle = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}
