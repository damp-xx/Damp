using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame
{
    public class TankPlayer : ITank
    {
        public Texture2D TextureTop { get; private set; }
        public Rectangle RectangleTop { get; private set; }
        public Vector2 PositionTop { get; private set; }
        public Vector2 OriginTop { get; private set; }
        public float RotationTop { get; private set; }

        public Texture2D TextureBottom { get; private set; }
        public Rectangle RectangleBottom { get; private set; }
        public Vector2 PositionBottom { get; private set; }
        public Vector2 OriginBottom { get; private set; }
        public float RotationBottom { get; private set; }

        private Vector2 tankVelocity;
        private const float tangetialVelocity = 5f;
        private float friction = 0.11f;

        private const float rotationSpeed = 0.045f;
        private const float rotationSpeedBottom = 0.02f;

        private Rectangle leftSideMap;
        private Rectangle rightSideMap;
        private Rectangle upSideMap;
        private Rectangle downSideMap;

        private Rectangle frontLeft;
        private Rectangle frontRight;
        private Rectangle backLeft;
        private Rectangle backRight;

        public TankPlayer()
        {
            Initialize();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureBottom, PositionBottom, null, Color.White, RotationBottom, OriginBottom, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureTop, PositionTop, null, Color.White, RotationTop, OriginTop, 1f, SpriteEffects.None, 0);
        }

        public void Update(List<IObstacle> obstacles)
        {
            RectangleBottom = new Rectangle((int)PositionBottom.X, (int)PositionBottom.Y, TextureBottom.Width, TextureBottom.Height);
            PositionBottom = tankVelocity + PositionBottom;
            OriginBottom = new Vector2(RectangleBottom.Width / 2, RectangleBottom.Height / 2);

            RectangleTop = new Rectangle((int)PositionTop.X, (int)PositionTop.Y, TextureTop.Width, TextureTop.Height);
            PositionTop = tankVelocity + PositionTop;
            OriginTop = new Vector2(TextureTop.Width / 2, TextureTop.Height / 1.22f);

            if (RectangleBottom.Intersects(leftSideMap))
            {
                tankVelocity.X = 0.5f;
            }
            else if (RectangleBottom.Intersects(rightSideMap))
            {
                tankVelocity.X = -0.5f;
            }
            else if (RectangleBottom.Intersects(upSideMap))
            {
                tankVelocity.Y = 0.5f;
            }
            else if (RectangleBottom.Intersects(downSideMap))
            {
                tankVelocity.Y = -0.5f;
            }
            else
            {
                /*************** Keyboard input ******************/
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    RotationBottom += rotationSpeedBottom;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    RotationBottom -= rotationSpeedBottom;

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                    RotationTop += rotationSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                    RotationTop -= rotationSpeed;

                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    tankVelocity.X = (float)Math.Sin(RotationBottom) * tangetialVelocity;
                    tankVelocity.Y = -(float)Math.Cos(RotationBottom) * tangetialVelocity;
                }
                else if (tankVelocity != Vector2.Zero) //Slows down the tank
                {
                    float i = tankVelocity.X;
                    float j = tankVelocity.Y;

                    tankVelocity.X = i -= friction * i;
                    tankVelocity.Y = j -= friction * j;
                }
            }        
        }

        public void LoadContent(ContentManager content)
        {
            TextureTop = content.Load<Texture2D>(@"Tanks\tank_top_1");
            TextureBottom = content.Load<Texture2D>(@"Tanks\tank_bottom_1");
            PositionTop = new Vector2(1000, 500);
            PositionBottom = new Vector2(1000  , 500);
            OriginTop = new Vector2(TextureTop.Width / 2, TextureTop.Height / 1.22f); // The 1.22f used, is because the top/canon of the tank doesn't have the origin in the middle of the texture
            OriginBottom = new Vector2(TextureBottom.Width / 2, TextureBottom.Height / 2);
        }

        private void Initialize()
        {
            leftSideMap = new Rectangle(0, 0, 50, 8000);
            rightSideMap = new Rectangle(8000, 0, 1000, 8000);
            upSideMap = new Rectangle(0, 0, 8000, 50);
            downSideMap = new Rectangle(0, 8050, 8000, 50);


        }
    }
}
