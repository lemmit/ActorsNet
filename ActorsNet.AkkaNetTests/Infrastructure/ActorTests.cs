using System;
using Akka.Actor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsNet.AkkaNet.Infrastructure.Tests
{
    public class Echo
    {
        public Echo(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }

    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            Receive<Echo>(echo =>
            {
                Sender.Tell(echo, Self);
            });
        }
    }

    [TestClass]
    public class ActorTests
    {
        Actor _actor = null;
        [ClassInitialize]
        public void InitializeTest()
        {
            var _actorSystem = Akka.Actor.ActorSystem.Create("TestSystem");
            var testActor = _actorSystem.ActorOf<EchoActor>("echo");
            _actor = new Actor(testActor);
        }

        [TestMethod]
        public void AskTest()
        {
            var messageText = "Echo";
            var result = _actor.Ask(new Echo(messageText), TimeSpan.FromSeconds(10)).Result;
            Assert.AreEqual(result.Message, messageText);
        }
    }
}