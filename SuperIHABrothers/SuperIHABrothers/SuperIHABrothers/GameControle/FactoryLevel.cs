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
        

        public FactoryLevel(ContentManager content, ICollisionControl collisionControl)
        {
            _content = content;
            _collisionControl = collisionControl;
            
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
            _list.Add(new SpriteFinishLine(_content.Load<Texture2D>("Finish"), new Vector2(1200, 340), 100, 100, mIAnchor));
            _listLisst.Add(_list);


            var _SpriteContainer = new SpriteContainer(_listLisst, mIAnchor);
            var Level = new Level1(_SpriteContainer, _SpriteContainer, _collisionControl);
            

            return Level;
        }
    }
}
