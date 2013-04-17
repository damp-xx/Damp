using Microsoft.Xna.Framework;

namespace TankGame.CameraP
{
    interface ICamera
    {
        Matrix transform { get; }

        void Update(Rectangle rectangle, Vector2 position);
    }
}
