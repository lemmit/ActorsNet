using System.Collections.Generic;
using System.Linq;
using ActorsNet.Infrastructure;

namespace ActorsNet.Services
{
    public class ActorSystemResolver
    {
        private readonly IEnumerable<ActorSystem> _actorSystems;

        public ActorSystemResolver(IEnumerable<ActorSystem> actorSystems)
        {
            _actorSystems = actorSystems;
        }

        public ActorSystem Resolve(string actorSystemName)
        {
            return _actorSystems.Single(service => service.Name == actorSystemName);
        }
    }
}