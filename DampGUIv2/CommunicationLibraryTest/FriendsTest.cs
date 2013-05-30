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
    class FriendsTest
    {
        private IFriend _mockFriend;
        private IPhoto _mockPhoto;
        private Friends _uut; 
        public void Setup()
        {
            _mockFriend = MockRepository.GenerateMock<IFriend>();
            _mockPhoto = MockRepository.GenerateMock<IPhoto>();
            _mockPhoto.Stub(x => x.LoadPicture());
            _mockPhoto.Stub(x => x.Create());
            _mockPhoto.Stub(x => x.IsMade).Return(false);
            _mockFriend.Stub(x => x.Photo).Return(_mockPhoto);
            _uut = new Friends();

        }

        [Test]
        public void Add_NothingInClass_1Added()
        {
                Setup();
            _uut.Add(_mockFriend);
            Assert.AreEqual(1,_uut.TotalFriends);
        }

        [Test]
        public void Get_IFriendInClass_GotAFriend()
        {
            Setup();
            _uut.Add(_mockFriend);
            Assert.AreEqual(_mockFriend, _uut.Get(0));
        }

        [Test]
        public void CurrentFriendIndex_IFriendInClass_GotCurrentFriend()
        {
            Setup();
            _uut.Add(_mockFriend);
            _uut.CurrentFriendIndex = 0;
            Assert.AreEqual(_mockFriend, _uut.CurrentFriend);
        }

        [Test]
        public void CurrentFriend_IFriendInClass_GotCurrentFriend()
        {
            Setup();
            _uut.Add(_mockFriend);
            _uut.CurrentFriend = _mockFriend;
            Assert.AreEqual(_mockFriend, _uut.CurrentFriend);
        }

        [Test]
        public void CurrentFriend_NothingInClass_ReturnNull()
        {
            Setup();
            Assert.AreEqual(null, _uut.CurrentFriend);
        }
    }
}
