using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TankGame.Game_Screens
{
    class PlayMenu : IMenu
    {
        private Viewport view;
        private ContentManager contentManager;
        private ICamera camera;
        private TankPlayer tankPlayer;
        private Map map;

        public PlayMenu(Viewport newView, ContentManager newContent)
        {
            view = newView;
            contentManager = newContent;
            
            Initialize();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            
            map.Draw(spriteBatch);
            tankPlayer.Draw(spriteBatch);
                        
            spriteBatch.End();
        }

        public void Update()
        {
            tankPlayer.Update();
            camera.Update(tankPlayer.RectangleBottom, tankPlayer.PositionBottom);
        }

        public void Initialize()
        {
            camera = new TankCamera(view);

            tankPlayer = new TankPlayer();
            tankPlayer.LoadContent(contentManager);

            map = new Map();
            map.LoadContent(contentManager);
        }
    }

}
