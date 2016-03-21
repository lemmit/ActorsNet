using System;
using ActorsNet.Infrastructure.Interfaces;

namespace ActorsNet.Infrastructure
{
    public class ActorSystem
    {
        private readonly IActorFinder _actorFinder;

        public ActorSystem(string actorSystemName, IActorFinder actorFinder)
        {
            _actorFinder = actorFinder;
            Name = actorSystemName;
        }

        public string Name { get; }

        public bool ActorExists(string actorPath)
        {
            IActor actor = null;
            try
            {
                actor = _actorFinder.FindActorByPath(actorPath);
            }
            catch (Exception)
            {
                return false;
            }
            return actor != null;
        }

        public void TellActor(string actorPath, object message)
        {
            var actor = _actorFinder.FindActorByPath(actorPath);
            Tell(actor, message);
        }

        private static void Tell(IActor actor, object message)
        {
            actor.Tell(message);
        }


        public void AskActor(string actorPath, object message, Action<object> responseReceived, TimeSpan timeOut)
        {
            var actor = _actorFinder.FindActorByPath(actorPath);
            Ask(actor, message, responseReceived, timeOut);
        }

        private static void Ask(IActor actor, object message, Action<object> responseReceived, TimeSpan timeOut)
        {
            var task = actor.Ask(message, timeOut);
            var result = task.Result;
            responseReceived(result);
        }
    }
}