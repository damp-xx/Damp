using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankGame.MapP;

namespace TankGame.TankP
{
    public class TankPlayer : ITank
    {
        private Texture2D testTexture;


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
        private const float tangetialVelocityBack = 2f;
        private float friction = 0.11f;

        private const float rotationSpeed = 0.045f;
        private const float rotationSpeedBottom = 0.02f;

        private Rectangle leftSideMap;
        private Rectangle rightSideMap;
        private Rectangle upSideMap;
        private Rectangle downSideMap;

        private Rectangle frontRect;
        private Rectangle backRect;
        private Rectangle leftRect;
        private Rectangle rightRect;

        private readonly float scale = 1.2f;

        public TankPlayer()
        {
            Initialize();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureBottom, RectangleBottom, null, Color.White, RotationBottom, OriginBottom, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureTop, RectangleTop, null, Color.White, RotationTop, OriginTop, SpriteEffects.None, 0);
            //spriteBatch.Draw(testTexture, frontRect, null, Color.White, RotationBottom, OriginTop, SpriteEffects.None, 0);
        }

        public void Update(List<IObstacle> obstacles)
        {
            RectangleBottom = new Rectangle((int)PositionBottom.X, (int)PositionBottom.Y, (int)(TextureBottom.Width/scale), (int)(TextureBottom.Height/scale));
            PositionBottom = tankVelocity + PositionBottom;
    
            RectangleTop = new Rectangle((int)PositionTop.X, (int)PositionTop.Y,(int)(TextureTop.Width/scale), (int)(TextureTop.Height/scale));
            PositionTop = tankVelocity + PositionTop;
            
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
                {
                    RotationBottom += rotationSpeedBottom;
                    RotationTop += rotationSpeedBottom;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    RotationBottom -= rotationSpeedBottom;
                    RotationTop -= rotationSpeedBottom;
                }

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
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    tankVelocity.X = -(float)Math.Sin(RotationBottom) * tangetialVelocityBack;
                    tankVelocity.Y = (float)Math.Cos(RotationBottom) * tangetialVelocityBack;
                }
            }        
        }

        public void LoadContent(ContentManager content)
        {
            TextureTop = content.Load<Texture2D>(@"Tanks\tank_top_1");
            TextureBottom = content.Load<Texture2D>(@"Tanks\tank_bottom_1");
            PositionTop = new Vector2(1000, 500);
            PositionBottom = new Vector2(1000  , 500);
            OriginTop = new Vector2((float)TextureTop.Width / 2, TextureTop.Height / 1.22f); // The 1.22f used, is because the top/canon of the tank doesn't have the origin in the middle of the texture
            OriginBottom = new Vector2((float)TextureBottom.Width / 2, TextureBottom.Height / 2);
        }

        private void Initialize()
        {
            leftSideMap = new Rectangle(-100+40, 0, 100, 2000);
            rightSideMap = new Rectangle(2000 , 0, 100, 2000);
            upSideMap = new Rectangle(0, -100+50, 2000, 100);
            downSideMap = new Rectangle(0 , 2000+40, 2000, 100);
        }
    }
}
