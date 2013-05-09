using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Sprites;
using SuperIHABrothers;
using GameState;
using Rhino.Mocks;
using Microsoft.Xna.Framework;

namespace SuperIHABrothers.Tests
{
    [TestFixture]
    public class AnchorTests
    {
        private Anchor _uut;
        private IKeybordInput _keybord;
        private GameTime _time;

        [SetUp]
        public void Init()
        {
            _keybord = MockRepository.GenerateMock<IKeybordInput>();
            _time = new GameTime();
            _uut = new Anchor(_keybord);
            
            
        }

        [Test]
        public void Update_LeftPressed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(false);
            _uut.Update(_time);
            Assert.AreEqual(-1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);

        }
        [Test]
        public void Update_RightPressed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(false);
            _keybord.Stub(x => x.IsRightPressed).Return(true);
            _uut.Update(_time);
            Assert.AreEqual(1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);

        }
        [Test]
        public void Update_LeftAndRightPressed_PositionNotChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(true);
            _uut.Update(_time);
            Assert.AreEqual(0.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);
        }
        public void Update_LeftPressedAndRelesed_PositionChanged()
        {
            _keybord.Stub(x => x.IsLeftPressed).Return(true);
            _keybord.Stub(x => x.IsRightPressed).Return(false);
            _uut.Update(_time);
            _keybord.Stub(x => x.IsLeftPressed).Return(false);
            _uut.Update(_time);
            Assert.AreEqual(-1.0f, _uut.Position.X);
            Assert.AreEqual(0.0f, _uut.Position.Y);
        }

    }
}
