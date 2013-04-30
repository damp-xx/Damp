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

namespace SuperIHABrothers.Tests
{
    [TestFixture]
    public class AnchorTests
    {
        private Anchor _uut;
        private IKeybordInput _keybord;
        private ISpriteAnchor _player;
        [SetUp]
        public void Init()
        {
            _keybord = MockRepository.GenerateMock<IKeybordInput>();
            _player = MockRepository.GenerateMock<ISpriteAnchor>();
            _uut = new Anchor(_keybord, _player);
            _player.Stub(x => x.Speed).Return(1);
            
        }

        [Test]
        public void Update_LeftPressed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(false);
            _uut.Update();
            Assert.AreEqual(-1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);

        }
        [Test]
        public void Update_RightPressed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(false);
            _keybord.Stub(x => x.IsRightPressed).Return(true);
            _uut.Update();
            Assert.AreEqual(1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);

        }
        [Test]
        public void Update_LeftAndRightPressed_PositionNotChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(true);
            _uut.Update();
            Assert.AreEqual(0.0f, _uut.Position.X);
            //Assert.AreEqual(0.0f, _uut.Position.Y);
        }
        public void Update_LeftPressedAndRelesed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(false);
            _uut.Update();
            _keybord.Stub(x => x.IsLeftPressed).Return(false);
            _uut.Update();
            Assert.AreEqual(-1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);
        }

    }
}
