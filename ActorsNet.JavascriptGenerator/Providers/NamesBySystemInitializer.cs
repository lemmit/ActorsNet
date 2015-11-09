using System.Collections.Generic;
using ActorsNet.Initializers.Interfaces;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;
using ActorsNet.Services.Interfaces;

namespace ActorsNet.JavascriptGenerator.Providers
{
    public class NamesBySystemProvider : INamesBySystemProvider, IMapper
    {
        private readonly Dictionary<string, List<string>> _listsOfMessagesNamesByActorSystem =
            new Dictionary<string, List<string>>();

        private readonly string tempSystemName;

        public NamesBySystemProvider(IEnumerable<INamedMapperInitializer> mappers)
        {
            foreach (var mapperInitializer in mappers)
            {
                tempSystemName = mapperInitializer.Name;
                mapperInitializer.Initialize(this);
            }
        }

        public void Add<T>()
        {
            if (!_listsOfMessagesNamesByActorSystem.ContainsKey(tempSystemName))
            {
                _listsOfMessagesNamesByActorSystem[tempSystemName] = new List<string>();
            }
            _listsOfMessagesNamesByActorSystem[tempSystemName].Add(typeof (T).FullName);
        }

        public Dictionary<string, List<string>> Get()
        {
            return _listsOfMessagesNamesByActorSystem;
        }
    }
}