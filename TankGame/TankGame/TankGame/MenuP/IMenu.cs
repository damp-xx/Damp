using Microsoft.Xna.Framework.Graphics;

namespace TankGame.MenuP
{
    interface IMenu
    {
        bool IsMouseVisible { get; }

        void Draw(SpriteBatch spriteBatch);

        void Update();

        void Initialize();
    }
}
