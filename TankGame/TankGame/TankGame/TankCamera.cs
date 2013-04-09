using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TankGame
{
    class TankCamera : ICamera
    {
        private Viewport view;
        private Vector2 centre;

        private int mapWidth = 8000;
        private int mapHeight = 8000;

        public Matrix transform { get; private set; }

        public TankCamera(Viewport newView)
        {
            view = newView;
        }

        public void Update(Rectangle rectangle, Vector2 position)
        {
            if (position.X < view.Width / 2)
                centre.X = centre.X;
            else if (position.X > (mapWidth - view.Width / 2))
                centre.X = mapWidth - view.Width;
            else
            {
                centre.X = position.X - (view.Width / 2);
            }

            if (position.Y < view.Height / 2)
                centre.Y = 0;
            else if (position.Y > (mapHeight - view.Height / 2))
                centre.Y = mapHeight - view.Height;
            else
            {
                centre.Y = position.Y - (view.Height / 2);
            }

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                                           Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));

         /*    centre = new Vector2(position.X + (rectangle.Width / 2 - (view.Width / 2)),
                position.Y + (rectangle.Height / 2 - (view.Height / 2)));
            
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                                           Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));*/
        }
    }
}
