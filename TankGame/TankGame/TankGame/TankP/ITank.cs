using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TankGame.MapP;

namespace TankGame.TankP
{
    interface ITank
    {
        Texture2D TextureTop { get; }
        Rectangle RectangleTop { get; }
        Vector2 PositionTop { get; }
        Vector2 OriginTop { get; }
        float RotationTop { get; }

        Texture2D TextureBottom { get; }
        Rectangle RectangleBottom { get; }
        Vector2 PositionBottom { get; }
        Vector2 OriginBottom { get; }
        float RotationBottom { get; }

        void Draw(SpriteBatch spriteBatch);
        void Update(List<IObstacle> obstacles);
        void LoadContent(ContentManager content);
    }
}
