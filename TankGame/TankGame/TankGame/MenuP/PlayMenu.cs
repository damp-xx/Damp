using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TankGame.CameraP;
using TankGame.MapP;
using TankGame.TankP;

namespace TankGame.MenuP
{
    class PlayMenu : IMenu
    {
        private Viewport view;
        private ContentManager contentManager;
        private ICamera camera;
        private TankPlayer tankPlayer;
        private MapNumber1 _mapNumber1;
        private IMenuManager menuManager;
        
 
        public bool IsMouseVisible { get; private set; }

        public PlayMenu(Viewport newView, ContentManager newContent, IMenuManager newMenuManager)
        {
            view = newView;
            contentManager = newContent;
            menuManager = newMenuManager;
            
            Initialize();
        }

       
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
            
            _mapNumber1.Draw(spriteBatch);
            tankPlayer.Draw(spriteBatch);
                        
            spriteBatch.End();
        }

        public void Update()
        {
            tankPlayer.Update(_mapNumber1.Obstacles);
            camera.Update(tankPlayer.RectangleBottom, tankPlayer.PositionBottom);
        }

        public void Initialize()
        {
            _mapNumber1 = new MapNumber1();
            _mapNumber1.LoadContent(contentManager);

            camera = new TankCamera(view);

            tankPlayer = new TankPlayer();
            tankPlayer.LoadContent(contentManager);

            IsMouseVisible = false;
        }
    }

}
