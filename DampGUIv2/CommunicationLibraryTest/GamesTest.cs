using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampGUI;
using NUnit.Framework;
using Rhino.Mocks;

namespace CommunicationLibraryTest
{
    [TestFixture]
    class GamesTest
    {
        private IGame _mockGame;
        private IPhotoCollection _mockPhotoCollection;
        private IPhoto _mockPhoto;
        private Games _uut;
        public void Setup()
        {
            _mockGame = MockRepository.GenerateMock<IGame>();
            _mockPhoto = MockRepository.GenerateMock<IPhoto>();
            _mockPhotoCollection = MockRepository.GenerateMock<IPhotoCollection>();
            _mockPhoto.Stub(x => x.LoadPicture());
            _mockPhoto.Stub(x => x.Create());
            _mockPhoto.Stub(x => x.IsMade).Return(false);
            _mockPhotoCollection.Stub(x => x.IsMade).Return(false);
            _mockPhotoCollection.Stub(x => x.Add(_mockPhoto));
            _mockGame.Stub(x => x.PhotoCollection).Return(_mockPhotoCollection);
            _uut = new Games();

        }

        [Test]
        public void Add_NothingInClass_1Added()
        {
            Setup();
            _uut.Add(_mockGame);
            Assert.AreEqual(1, _uut.TotalGames);
        }

        [Test]
        public void Get_1GameInClass_GotAGame()
        {
            Setup();
            _uut.Add(_mockGame);
            Assert.AreEqual(_mockGame, _uut.Get(0));
        }

        [Test]
        public void CurrentIndex_IGameInClass_GotCurrentGame()
        {
            Setup();
            _uut.Add(_mockGame);
            _uut.CurrentIndex = 0;
            Assert.AreEqual(_mockGame, _uut.CurrentGame);
        }

        [Test]
        public void CurrentFriend_IGameInClass_GotCurrentGame()
        {
            Setup();
            _uut.Add(_mockGame);
            _uut.CurrentGame = _mockGame;
            Assert.AreEqual(_mockGame, _uut.CurrentGame);
        }

        [Test]
        public void CurrentGame_NothingInClass_ReturnNull()
        {
            Setup();
            Assert.AreEqual(null, _uut.CurrentGame);
        }
    }
}
