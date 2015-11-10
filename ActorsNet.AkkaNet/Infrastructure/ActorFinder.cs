using System;
using System.Threading.Tasks;
using ActorsNet.Infrastructure.Interfaces;
using ActorsNet.Exceptions;
using ActorSystem = Akka.Actor.ActorSystem;
using IActorRef = Akka.Actor.IActorRef;

namespace ActorsNet.AkkaNet.Infrastructure
{
    public class ActorFinder : IActorFinder
    {
        private readonly ActorSystem _actorSystem;
        private readonly TimeSpan _timeOut;

        public ActorFinder(ActorSystem akkaActorSystem)
        {
            _actorSystem = akkaActorSystem;
            _timeOut = TimeSpan.FromSeconds(10);
        }

        public ActorFinder(ActorSystem akkaActorSystem, TimeSpan actorFindTimeOut)
        {
            _actorSystem = akkaActorSystem;
            _timeOut = actorFindTimeOut;
        }

        public IActor FindActorByPath(string actorPath)
        {
            var actor = FindAkkaActorByPath(actorPath);
            return new Actor(actor);
        }

        private IActorRef FindAkkaActorByPath(string actorPath)
        {
            var fullPath = PrepareFullPath(actorPath);
            var actor = FindActorUsingActorSystem(fullPath);
            return actor;
        }

        private string PrepareFullPath(string actorPath)
        {
            return "akka://" + _actorSystem.Name +
                   "/user" + actorPath;
        }

        private IActorRef FindActorUsingActorSystem(string fullPath)
        {
            try
            {
                var actorSelection = _actorSystem.ActorSelection(fullPath);
                var actorFindTask = Task.Run(() => actorSelection.ResolveOne(_timeOut));
                return actorFindTask.Result;
            }
            catch (Exception e)
            {
                throw new ActorNotFoundException(fullPath, e);
            }
        }
    }
}