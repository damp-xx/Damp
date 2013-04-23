using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Sprites;
using SuperIHABrothers;
namespace SuperIHABrothers.Tests
{
    [TestFixture]
    public class AnchorTests
    {
        private Anchor _uut;
        [SetUp]
        public void Init()
        {
            _uut = new Anchor();
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void SetSpeed_InvalidInput_ExceptionThrown()
        {
            _uut.SetSpeed(-1);
        }

    }
}
