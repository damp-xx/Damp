using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprites;
using SuperIHABrothers;
using GameState;


namespace GameControle
{
    public class FactoryLevel : Microsoft.Xna.Framework.Game, IFactoryLevel
    {
        public ILevel GetLevelOne(IKeybordInput _input, ContentManager manager)
        {
            
            var pos = new Vector2(400, 10);
            var mIAnchor = new Anchor(_input);
            var EnOff = new Vector2(325, 300);
            var mPlayer = new SpritePlayer(manager.Load<Texture2D>("burning man"), pos, 47, 44, mIAnchor, _input);
            var mEnvironment = new SpriteEnviroment(manager.Load<Texture2D>("EnvironmentLong"), EnOff, 40, 150, mIAnchor);
            

            var _list = new List<ISprite>();
            var _listLisst = new List<List<ISprite>>();
            // Add Player list 
            _list.Add(mPlayer);
            _listLisst.Add(_list);
            // Add Monster list
            _list = new List<ISprite>();
            _listLisst.Add(_list);
            // Add Environment list
            _list = new List<ISprite>();
            _list.Add(mEnvironment);
            _listLisst.Add(_list);
            // Add Background list
            _list = new List<ISprite>();
            _listLisst.Add(_list);
            // Add DeathAnimation
            _list = new List<ISprite>();
            _listLisst.Add(_list);


            var _SpriteContainer = new SpriteContainer(_listLisst, mIAnchor);
            var Level = new Level1(_SpriteContainer, _SpriteContainer);

            return Level;
        }
    }
}
