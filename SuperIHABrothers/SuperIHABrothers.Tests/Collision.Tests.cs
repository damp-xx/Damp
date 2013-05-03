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
        private ISprite _stubSpritePlayer;
        private ISprite _stubSpriteEnvironment;
        private ISprite _stubSpriteMonster;
        private ICollisionDetect _mockPlayerEnvironmentDetect;
        private Rectangle _stubRectangle;
        

        private void Setup()
        {
            _stubSpriteContainerCollision = MockRepository.GenerateMock<ISpriteContainerCollision>();
            _stubSpritePlayerList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpriteMonsterList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpriteEnvironmentList = MockRepository.GenerateMock<List<ISprite>>();
            _stubSpritePlayer = MockRepository.GenerateMock<ISprite>();
            _stubSpriteEnvironment = MockRepository.GenerateMock<ISprite>();
            _stubSpriteMonster = MockRepository.GenerateMock<ISprite>();
            _mockPlayerEnvironmentDetect = MockRepository.GenerateMock<ICollisionDetect>();
            _stubRectangle = new Rectangle(0,0,10,10);
        
            _stubSpritePlayerList.Add(_stubSpritePlayer);
            _stubSpriteMonsterList.Add(_stubSpriteEnvironment);
            _stubSpriteEnvironmentList.Add(_stubSpriteMonster);
           
            _stubSpriteContainerCollision.Stub(x => x.SpriteList[(int)listTypes.Player]).Return(_stubSpritePlayerList);
            _stubSpriteContainerCollision.Stub(x => x.SpriteList[(int)listTypes.Environment]).Return(_stubSpriteEnvironmentList);
            _stubSpriteContainerCollision.Stub(x => x.SpriteList[(int)listTypes.Monster]).Return(_stubSpriteMonsterList);

            _stubSpritePlayer.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteEnvironment.Stub(x => x.MyRectangle).Return(_stubRectangle);
            _stubSpriteMonster.Stub(x => x.MyRectangle).Return(_stubRectangle);
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
