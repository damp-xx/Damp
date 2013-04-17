using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame.ButtonP
{
    public class Button : IButton
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;

        private Color color = new Color(255, 255, 255, 255);
        public Vector2 size;

        private bool down;
        public bool IsClicked { get; private set; }

        public Button(Texture2D newTexture, GraphicsDevice graphics)
        {
            texture = newTexture;

            size = new Vector2((float) graphics.Viewport.Width/8, (float) graphics.Viewport.Height/30);
        }     

        public void Update(MouseState mouse)
        {
            rectangle = new Rectangle((int) position.X, (int) position.Y, (int) size.X, (int) size.Y);

            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (color.A == 255) down = false;
                if (color.A == 0) down = true;
                if (down) color.A += 3;
                else color.A -= 3;
                if (mouse.LeftButton == ButtonState.Pressed) IsClicked = true;
            }
            else if (color.A < 255)
            {
                color.A += 3;
                IsClicked = false;
            }
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, color);
        }
    }
}