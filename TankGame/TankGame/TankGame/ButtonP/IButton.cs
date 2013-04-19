using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame.ButtonP
{
    interface IButton
    {
        bool IsClicked { get; }

        void Update(MouseState mouse);

        void setPosition(Vector2 newPosition);

        void Draw(SpriteBatch spriteBatch);
    }
}
