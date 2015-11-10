using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Akka.Actor;
using ActorNotFoundException = ActorsNet.Exceptions.ActorNotFoundException;

namespace ActorsNet.AkkaNet.Infrastructure.Tests
{
    [TestClass]
    public class ActorFinderTests
    {
        ActorFinder _finder = null;
        private const string testActorName = "echo";
        private const string testSystemName = "TestSystem";

        [TestInitialize]
        public void InitializeTest()
        {
            var _actorSystem = Akka.Actor.ActorSystem.Create(testSystemName);
            var testActor = _actorSystem.ActorOf<EchoActor>(testActorName);
            _finder = new ActorFinder(_actorSystem);
        }

        [TestMethod]
        public void FindActorByPathTest()
        {
            var actor = _finder.FindActorByPath("/" + testActorName);
            Assert.IsNotNull(actor);
        }

        [TestMethod]
        [ExpectedException(typeof(ActorNotFoundException))]
        public void ActorNotFoundByPathTest()
        {
            //(enough)random string to assert that actor with that name doesn't exist in the system
            _finder.FindActorByPath("/oqeurghuerhfglkjhdflkgjhsdfg");
            Assert.Fail("ActorNotFoundException should be raised");
        }
    }
}