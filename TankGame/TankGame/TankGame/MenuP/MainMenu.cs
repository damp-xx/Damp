using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TankGame.ButtonP;

namespace TankGame.MenuP 
{
    class MainMenu : IMenu
    {
        private Viewport view;
        private ContentManager contentManager;
        private GraphicsDevice graphics;

        private IMenuManager menuManager;

        /* Buttons */
        private IButton playSingleButton;
        private IButton playMultiButton;
        private IButton SettingsButton;
        private IButton exitButton;
        private List<IButton> allButtons;

        public bool IsMouseVisible { get; private set; }

        public MainMenu(Viewport newView, ContentManager newContent, IMenuManager newMenuManager, GraphicsDevice newGraphics)
        {
            view = newView;
            contentManager = newContent;
            menuManager = newMenuManager;
            graphics = newGraphics;

            Initialize();
        }

   

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var button in allButtons)
            {
                button.Draw(spriteBatch);
            }

            spriteBatch.End();
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            foreach (var button in allButtons)
            {
                button.Update(mouse);
            }

            if (playSingleButton.IsClicked) 
                menuManager.ShowMenu(new PlayMenu(view,contentManager,menuManager));
        }

        public void Initialize()
        {
            allButtons = new List<IButton>();
            playSingleButton = new Button(contentManager.Load<Texture2D>(@"Buttons\singlePlayerButton"), graphics);
            SetButtonsPosition();
            allButtons.Add(playSingleButton);

            IsMouseVisible = true;
        }

        private void SetButtonsPosition()
        {
            playSingleButton.setPosition(new Vector2(550, view.Height/5));
        }
    }
}
