using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Sprites;
using SuperIHABrothers;
using SuperIHABrothers.GameState;
using Rhino.Mocks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SuperIHABrothers.GameState;



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
        private ISprite _sprite1;
        private ISprite _sprite2;
        private ISprite _sprite;
        private GameTime _time;

        [SetUp]
        public void Setup()
        {
            _list = new List<List<ISprite>>();
            _listBackground = new List<ISprite>();
            _listDeathAnimation = new List<ISprite>();
            _sprite1 = MockRepository.GenerateMock<ISprite>();
            _sprite2 = MockRepository.GenerateMock<ISprite>();
            _sprite = MockRepository.GenerateMock<ISprite>();
            _anchor = MockRepository.GenerateMock<IAnchorUpdate>();
            

            _uut = new SpriteContainer(_list, _anchor);
            _time = new GameTime();
        }



        [Test]
        public void Update_AnchorUpdata_Success()
        {
            _uut.Update(_time);
            _anchor.AssertWasCalled(x=>x.Update(_time));
        }

        [Test]
        public void Update_Sprite1Updata_Success()
        {
            _uut.Update(_time);
            _sprite1.AssertWasCalled(x => x.Update(_time));
        }

        [Test]
        public void Update_Sprite2Updata_Success()
        {
            _uut.Update(_time);
            _sprite2.AssertWasCalled(x => x.Update(_time));
        }

        [Test]
        public void AddSprite_SpriteAdded_Success()
        {
            //_uut.AddSprite(_sprite);
            Assert.AreEqual(_sprite, _list[2]);
        }

        [Test]
        public void RemoveSprite_SpriteRemoved_OnlyOneSpriteLeft()
        {
            //_uut.RemoveSprite(_sprite1);
            Assert.AreEqual(1, _list.Count);
            Assert.AreEqual(_sprite2, _list[0]);
        }
    }
}
