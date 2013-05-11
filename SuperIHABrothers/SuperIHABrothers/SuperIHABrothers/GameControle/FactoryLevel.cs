using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientCommunication;
using Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprite;
using Sprites;
using SuperIHABrothers;
using GameState;


namespace GameControle
{
    public class FactoryLevel : Microsoft.Xna.Framework.Game, IFactoryLevel
    {
        private ContentManager _content;
        private ICollisionControl _collisionControl;
        private GameStateC _gameState;
        

        public FactoryLevel(ContentManager content, ICollisionControl collisionControl, GameStateC gameState)
        {
            _content = content;
            _collisionControl = collisionControl;
            _gameState = gameState;

        }

        public ILevel GetLevelOne(IKeybordInput _input)
        {        
            var pos = new Vector2(400, 10);
            var mIAnchor = new Anchor(_input);
            var mPlayer = new SpritePlayer(_content.Load<Texture2D>("burning man"), pos, 47, 44, mIAnchor, _input);
           
           
            var _list = new List<ISprite>();
            var _listLisst = new List<List<ISprite>>();

            // Add Background list
            _list = new List<ISprite>();
            _list.Add(new SpriteBackground(_content.Load<Texture2D>("background"), new Vector2(-1024, 0), 600, 1024, mIAnchor, _content.Load<Texture2D>("background")));
            _listLisst.Add(_list);

            // Add Player list 
            _list = new List<ISprite>();
            _list.Add(mPlayer);
            _listLisst.Add(_list);
            
            // Add Monster list
            _list = new List<ISprite>();
            _listLisst.Add(_list);
            
            // Add Environment list
            _list = new List<ISprite>();
            for (int i = 0; i < 6000; i += 600) // "Floor" environtment
            {
                _list.Add(new SpriteEnviroment(_content.Load<Texture2D>("EnvironmentLong"), new Vector2(i, 440), 40, 600, mIAnchor));
            }

            int height = 400;
            for (int i = 500; i < 5000; i+=200 )
            {
                if (height < 50)
                    height = 400;
                _list.Add(new SpriteEnviroment(_content.Load<Texture2D>("EnvironmentLong"), new Vector2(i, height), 40, 150, mIAnchor));
                height -= 100;
            }

            _list.Add(new SpriteEnviroment(_content.Load<Texture2D>("EnvironmentLong"), new Vector2(-300, 250), 240, 300, mIAnchor)); // Start "border" environment
            _list.Add(new SpriteEnviroment(_content.Load<Texture2D>("EnvironmentLong"), new Vector2(6000, 250), 240, 300, mIAnchor)); // Stop "border" environment
           _listLisst.Add(_list);
           
            // Add DeathAnimation
            _list = new List<ISprite>();
            _listLisst.Add(_list);


            // Add Finishline
            _list = new List<ISprite>();
            _list.Add(new SpriteFinishLine(_content.Load<Texture2D>("Finish"), new Vector2(2000, 340), 100, 100, mIAnchor));
            _listLisst.Add(_list);

            // Add CD's
            _list = new List<ISprite>();
            int heightCD = 350;
            for (int i = 520; i < 5000; i += 200)
            {
                if (heightCD < 50)
                    heightCD = 350;
                _list.Add(new SpriteCD(_content.Load<Texture2D>("cd_small"), new Vector2(i, heightCD), 30, 30, mIAnchor));
                _list.Add(new SpriteCD(_content.Load<Texture2D>("cd_small"), new Vector2(i+80, heightCD), 30, 30, mIAnchor));
                heightCD -= 100;
            }
            _listLisst.Add(_list);

            // Add Font
            _list = new List<ISprite>();
            _list.Add(new SpriteScore(_content.Load<SpriteFont>("ScoreFont"), new Vector2(10, 10), _gameState));
            _listLisst.Add(_list);

            var _SpriteContainer = new SpriteContainer(_listLisst, mIAnchor);
            var Level = new Level1(_SpriteContainer, _SpriteContainer, _collisionControl);
            return Level;
        }
    }
}
