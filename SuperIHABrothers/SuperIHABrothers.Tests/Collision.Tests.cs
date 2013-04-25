using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Collision;
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
        private ISprite _stubSpritePlayer;
        private ISprite _stubSpriteEnvironment;
        private ISprite _stubSpriteMonster;
        private ICollisionDetect _mockPlayerEnvironmentDetect;

        private void Setup()
        {
            _stubSpriteContainerCollision = MockRepository.GenerateMock<ISpriteContainerCollision>();
            _stubSpritePlayer = MockRepository.GenerateMock<ISprite>();
            _stubSpriteEnvironment = MockRepository.GenerateMock<ISprite>();
            _stubSpriteMonster = MockRepository.GenerateMock<ISprite>();
            _mockPlayerEnvironmentDetect = MockRepository.GenerateMock<ICollisionDetect>();
        }

        //[Test]
        public void Update_InstantiatedWithSpritePlayerAndSpriteEnvironment_PlayerEnvironmentWasCalled()
        {
            Setup();
            //_stubSpriteContainerCollision.Stub(x => x.SpriteList.Count).Return(2);
            _stubSpriteContainerCollision.SpriteList.Add(_stubSpritePlayer);
            _stubSpriteContainerCollision.SpriteList.Add(_stubSpriteEnvironment);
            //_mockType.Stub(x => x.Compose(Arg<List<string>>.Is.Equal("Work"))).Return(returnList);
            _uutCollisionControl = new CollisionControl(_stubSpriteContainerCollision, _mockPlayerEnvironmentDetect);

            _uutCollisionControl.Update();

            _mockPlayerEnvironmentDetect.AssertWasCalled(x => x.Detect(Arg<ISpriteContainerCollision>.Is.Anything, Arg<ISprite>.Is.Anything, Arg<ISprite>.Is.Anything));
        }
	}

    [TestFixture]
    class PlayerEnvironmentDetectTests
    {}
}
