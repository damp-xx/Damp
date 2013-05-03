using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Collision;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Rhino.Mocks;
using Sprites;

namespace SuperIHABrothers.Tests
{
    [TestFixture]
	class CollisionControlTests
    {
        private CollisionControl _uutCollisionControl;
        private ISpriteContainerCollision _stubSpriteContainerCollision;
        private List<ISprite> _stubSpriteList; 
        private ISprite _stubSpritePlayer;
        private ISprite _stubSpriteEnvironment;
        private ISprite _stubSpriteMonster;
        private ICollisionDetect _mockPlayerEnvironmentDetect;
        private Rectangle _stubRectangle;
        

        private void Setup()
        {
            _stubSpriteContainerCollision = MockRepository.GenerateMock<ISpriteContainerCollision>();
            _stubSpriteList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpritePlayer = MockRepository.GenerateMock<ISprite>();
            _stubSpriteEnvironment = MockRepository.GenerateMock<ISprite>();
            _stubSpriteMonster = MockRepository.GenerateMock<ISprite>();
            _mockPlayerEnvironmentDetect = MockRepository.GenerateMock<ICollisionDetect>();
            _stubRectangle = new Rectangle(0,0,10,10);
        
            _stubSpriteList.Add(_stubSpritePlayer);
            _stubSpriteList.Add(_stubSpriteEnvironment);
            _stubSpriteList.Add(_stubSpriteMonster);
            _stubSpriteContainerCollision.Stub(x => x.SpriteList).Return(_stubSpriteList);

            _stubSpritePlayer.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteEnvironment.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteMonster.Stub(x => x.MyRectangle).Return(_stubRectangle);
            //_stubSpritePlayer.Stub(x => x.MyRectangle.Intersects(Arg<Rectangle>.Is.Equal(_stubSpriteEnvironment.MyRectangle))).Return(true);        

            //_stubSpritePlayer.Stub(x => x.GetType()).Return();
            //_stubSpriteEnvironment.Stub(x => x.GetType()).Return(typeof(SpriteEnvironment));
            //_stubSpriteMonster.Stub(x => x.GetType()).Return(typeof(SpriteMonster));
        }

        [Test]
        public void Update_InstantiatedWithSpritePlayerAndSpriteEnvironment_PlayerEnvironmentWasCalled()
        {
            Setup();

            _uutCollisionControl = new CollisionControl(_stubSpriteContainerCollision, _mockPlayerEnvironmentDetect);

            _uutCollisionControl.Update();

            _mockPlayerEnvironmentDetect.AssertWasCalled(x => x.Detect(Arg<ISpriteContainerCollision>.Is.Anything, Arg<ISprite>.Is.Anything, Arg<ISprite>.Is.Anything));
        }
	}

    [TestFixture]
    class PlayerEnvironmentDetectTests
    {}
}
