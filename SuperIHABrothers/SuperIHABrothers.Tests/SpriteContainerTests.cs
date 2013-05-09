using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Sprites;
using SuperIHABrothers;

using Rhino.Mocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using GameState;





namespace SuperIHABrothers.Tests
{
    [TestFixture]
    public class SpriteContainerTests
    {
        private List<List<ISprite>> _list;
        private List<ISprite> _listPlayer;
        private List<ISprite> _listMonsters;
        private List<ISprite> _listEnviroment;
        private List<ISprite> _listDeathAnimation;
        private List<ISprite> _listBackground;

        private IAnchorUpdate _anchor;
        private SpriteContainer _uut;
        private ISprite _spritePlayer;
        private ISprite _spriteEnvironment;
        private ISprite _spriteMonster;
        private ISprite _spriteDeathAnimation;
        private ISprite _spriteBackground;
        private GameTime _time;
        private SpriteBatch _spriteBatch;
        

        [SetUp]
        public void Setup()
        {
            _list = new List<List<ISprite>>();
            _listBackground = new List<ISprite>();
            _listDeathAnimation = new List<ISprite>();
            _listEnviroment = new List<ISprite>();
            _listMonsters = new List<ISprite>();
            _listPlayer = new List<ISprite>();
            _spritePlayer = MockRepository.GenerateMock<ISprite>();
            _spriteEnvironment = MockRepository.GenerateMock<ISprite>();
            _spriteMonster = MockRepository.GenerateMock<ISprite>();
            _spriteDeathAnimation = MockRepository.GenerateMock<ISprite>();
            _spriteBackground = MockRepository.GenerateMock<ISprite>();
            _anchor = MockRepository.GenerateMock<IAnchorUpdate>();
            
            _listBackground.Add(_spriteBackground);
            _listDeathAnimation.Add(_spriteDeathAnimation);
            _listEnviroment.Add(_spriteEnvironment);
            _listMonsters.Add(_spriteMonster);
            _listPlayer.Add(_spritePlayer);
            _list.Add(_listPlayer);
            _list.Add(_listMonsters);
            _list.Add(_listEnviroment);
            _list.Add(_listBackground);
            _list.Add(_listDeathAnimation);


            _uut = new SpriteContainer(_list, _anchor);
            _time = new GameTime();
            Game game = new Game();
            _spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }



        [Test]
        public void Update_AnchorUpdata_Success()
        {
            _uut.Update(_time);
            _anchor.AssertWasCalled(x=>x.Update(_time));
        }

        [Test]
        public void Update_PlayerSprite_UpdateCalled()
        {
            _uut.Update(_time);
            _spritePlayer.AssertWasCalled(x=>x.Update(_time));
        }

        [Test]
        public void Update_MonsterSprite_UpdateCalled()
        {
            _uut.Update(_time);
            _spriteMonster.AssertWasCalled(x => x.Update(_time));
        }

        [Test]
        public void Update_EnvironmentSprite_UpdateCalled()
        {
            _uut.Update(_time);
            _spriteEnvironment.AssertWasCalled(x => x.Update(_time));
        }

        [Test]
        public void Update_BackgroundSprite_UpdateCalled()
        {
            _uut.Update(_time);
            _spriteBackground.AssertWasCalled(x => x.Update(_time));
        }


        [Test]
        public void Update_DeathAnimationSprite_UpdateCalled()
        {
            _uut.Update(_time);
            _spriteDeathAnimation.AssertWasCalled(x => x.Update(_time));
        }

        [Test]
        public void Draw_MonsterSprite_DrawCalled()
        {
            _uut.Draw(_spriteBatch);
            _spriteMonster.AssertWasCalled(x => x.Draw(_spriteBatch));
        }

        [Test]
        public void Draw_PlayerSprite_DrawCalled()
        {
            _uut.Draw(_spriteBatch);
            _spritePlayer.AssertWasCalled(x => x.Draw(_spriteBatch));
        }

        [Test]
        public void Draw_EnvironmentSprite_DrawCalled()
        {
            _uut.Draw(_spriteBatch);
            _spriteEnvironment.AssertWasCalled(x => x.Draw(_spriteBatch));
        }

        [Test]
        public void Draw_BackgroundSprite_DrawCalled()
        {
            _uut.Draw(_spriteBatch);
            _spriteBackground.AssertWasCalled(x => x.Draw(_spriteBatch));
        }

        [Test]
        public void Draw_DeathAnimaitonSprite_DrawCalled()
        {
            _uut.Draw(_spriteBatch);
            _spriteDeathAnimation.AssertWasCalled(x => x.Draw(_spriteBatch));
        }
        
        [Test]
        public void AddDeathAnimation_SpriteAdded_Success()
        {
            ISprite _sprite = MockRepository.GenerateMock<ISprite>();
            _uut.AddDeathAnimation(_sprite); 
            Assert.AreEqual(_sprite, _uut.SpriteList[(int)listTypes.DeathAnimation][1]);
        }

        [Test]
        public void RemoveMonster_SpriteRemoved_Success()
        {
            _uut.RemoveMonster(_spriteMonster);
            Assert.AreEqual(0, _uut.SpriteList[(int)listTypes.Monster].Count);
        }

        [Test]
        public void RemovePlayer_SpriteRemoved_Success()
        {
            _uut.RemovePlayer(_spritePlayer);
            Assert.AreEqual(0, _uut.SpriteList[(int)listTypes.Player].Count);
        }

        [Test]
        public void RemovePlayer_WrongSprite_ExceptionThrown()
        {
            _uut.RemovePlayer(_spriteDeathAnimation);
        }
    }
}
