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
        private List<ISprite> _stubSpritePlayerList;
        private List<ISprite> _stubSpriteMonsterList;
        private List<ISprite> _stubSpriteEnvironmentList;
        private List<List<ISprite>> _stubAllSpritesList; 
        private ISprite _stubSpritePlayer;
        private ISprite _stubSpriteEnvironment;
        private ISprite _stubSpriteMonster;
        private ICollisionDetect _mockPlayerEnvironmentDetect;
        private ICollisionDetect _mockPlayerMonsterDetect;
        private ICollisionDetect _mockMonsterEnvironmentDetect;
        private Rectangle _stubRectangle;
        

        private void Setup()
        {
            _stubSpriteContainerCollision = MockRepository.GenerateMock<ISpriteContainerCollision>();
            _stubSpritePlayerList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpriteMonsterList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpriteEnvironmentList = MockRepository.GenerateMock<List<ISprite>>();
            _stubAllSpritesList = MockRepository.GenerateMock < List<List<ISprite>>>();
            _stubSpritePlayer = MockRepository.GenerateMock<ISprite>();
            _stubSpriteEnvironment = MockRepository.GenerateMock<ISprite>();
            _stubSpriteMonster = MockRepository.GenerateMock<ISprite>();
            _mockPlayerEnvironmentDetect = MockRepository.GenerateMock<ICollisionDetect>();
            _mockPlayerMonsterDetect = MockRepository.GenerateMock<ICollisionDetect>();
            _mockMonsterEnvironmentDetect = MockRepository.GenerateMock<ICollisionDetect>();
            _stubRectangle = new Rectangle(0,0,10,10);
        
            _stubSpritePlayerList.Add(_stubSpritePlayer);
            _stubSpriteMonsterList.Add(_stubSpriteEnvironment);
            _stubSpriteEnvironmentList.Add(_stubSpriteMonster);

            _stubAllSpritesList.Add(_stubSpritePlayerList);
            _stubAllSpritesList.Add(_stubSpriteMonsterList);
            _stubAllSpritesList.Add(_stubSpriteEnvironmentList);

            _stubSpriteContainerCollision.Stub(x => x.SpriteList).Return(_stubAllSpritesList);

            _stubSpritePlayer.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteEnvironment.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteMonster.Stub(x => x.MyRectangle).Return(_stubRectangle);

            _uutCollisionControl = new CollisionControl(_stubSpriteContainerCollision, _mockPlayerEnvironmentDetect, _mockPlayerMonsterDetect, _mockMonsterEnvironmentDetect);
            
        }

        
        [Test]
        public void Update_InstantiatedWithSpritePlayerAndSpriteEnvironment_PlayerEnvironmentWasCalled()
        {
            Setup();
            
            _uutCollisionControl.Update();

            _mockPlayerEnvironmentDetect.AssertWasCalled(x => x.Detect(Arg<ISpriteContainerCollision>.Is.Anything, Arg<ISprite>.Is.Anything, Arg<ISprite>.Is.Anything));
        }

        [Test]
        public void Update_InstantiatedWithSpritePlayerAndSpriteMonster_PlayerMonsterWasCalled()
        {
            Setup();

            _uutCollisionControl.Update();

            //_mockPlayerEnvironmentDetect.AssertWasCalled(x => x.Detect(_stubSpriteContainerCollision, _stubSpritePlayer, _stubSpriteMonster));
            _mockPlayerMonsterDetect.AssertWasCalled(x => x.Detect(Arg<ISpriteContainerCollision>.Is.Anything, Arg<ISprite>.Is.Anything, Arg<ISprite>.Is.Anything));
        }
	}

    [TestFixture]
    class PlayerEnvironmentDetectTests
    {}
}
