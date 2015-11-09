using System;
using System.Threading.Tasks;
using ActorsNet.Infrastructure.Interfaces;
using Akka.Actor;

namespace ActorsNet.AkkaNet.Infrastructure
{
    public class Actor : IActor
    {
        private readonly IActorRef _akkaActor;

        public Actor(IActorRef akkaActor)
        {
            _akkaActor = akkaActor;
        }

        public Task<T> Ask<T>(T message)
        {
            var task = _akkaActor.Ask<T>(message);
            return task;
        }

        public Task<T> Ask<T>(T message, TimeSpan timeOut)
        {
            var task = _akkaActor.Ask<T>(message, timeOut);
            return task;
        }

        public void Tell(object message)
        {
            _akkaActor.Tell(message);
        }
    }
}