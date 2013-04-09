using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DampServer;
using NUnit.Framework;
using Rhino.Mocks;

namespace DampServerTest
{
    [TestFixture]
    public class ChatCommandTest
    {
        [Test]
        [ExpectedException]
        public void TestTestTest()
        {
            Console.WriteLine("Hej Hej  35");
            throw new Exception();
        }

        [Test]
        public void CanHandle_success()
        {
            var chat = new ChatCommand();
            Assert.IsTrue(chat.CanHandleCommand("Chat"));
        }

        [Test]
        public void CanHandle_failed()
        {
            var chat = new ChatCommand();
            Assert.IsFalse(chat.CanHandleCommand("ChatNotCmd"));
        }

        [Test]
        public void Property_NeedsAuthtication_success()
        {
            var chat = new ChatCommand();
            Assert.IsTrue(chat.NeedsAuthcatication);
        }

        [Test]
        public void Property_IsPersistant_success()
        {
            var chat = new ChatCommand();
            Assert.IsFalse(chat.IsPersistant);
        }

        [Test]
        [ExpectedException(typeof(InvalidHttpRequestException))]
        public void Execute_Missing_Argrument_Message_success()
        {
            var HttpMocks = MockRepository.GenerateMock<ICommandArgument>();
            HttpMocks.Stub(x=>x.Query.Get(Arg<string>.Is.Equal("To"))).Return("1");


            var chat = new ChatCommand();

            chat.Execute(HttpMocks, "Chat");
        }

        [Test]
        [ExpectedException(typeof(InvalidHttpRequestException))]
        public void Execute_Missing_Argrument_To_success()
        {
            var HttpMocks = MockRepository.GenerateMock<ICommandArgument>();
            HttpMocks.Stub(x => x.Query.Get(Arg<string>.Is.Equal("Message"))).Return("1");


            var chat = new ChatCommand();

            chat.Execute(HttpMocks, "Chat");
        }

        [Test]
        [ExpectedException(typeof(InvalidHttpRequestException))]
        public void Execute_Missing_Argrument_ALL_success()
        {
            var HttpMocks = MockRepository.GenerateMock<ICommandArgument>();
            HttpMocks.Stub(x => x.Query.Get(Arg<string>.Is.Anything)).Return("");


            var chat = new ChatCommand();

            chat.Execute(HttpMocks, "Chat");
        }


    }
}
