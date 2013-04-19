using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame.MapP
{
   public interface IObstacle
    {
        Texture2D Texture { get; }
        Rectangle Rectangle { get; }
        Vector2 Position { get; }
        Vector2 Origin { get; }
        float Rotation { get; }

        void Draw(SpriteBatch spriteBatch);
        void Update();
        void LoadContent(ContentManager content);
    }
}
