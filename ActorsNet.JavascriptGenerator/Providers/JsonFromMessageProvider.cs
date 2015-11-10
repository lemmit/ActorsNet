using System;
using System.Collections.Generic;
using ActorsNet.Initializers.Interfaces;
using ActorsNet.JavascriptGenerator.Factories.Interfaces;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;
using ActorsNet.Services.Interfaces;

namespace ActorsNet.JavascriptGenerator.Providers
{
    /// <summary>
    /// Provides Json stringified objects from given message type name
    /// </summary>
    public class JsonFromMessageProvider : IJsonStringOfMessageProvider, IMessageMapper
    {
        private readonly Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        private readonly IJsonStringifiedObjectFactory _factory;

        public JsonFromMessageProvider(IEnumerable<IMapperInitializer> mappers,
            IJsonStringifiedObjectFactory factory
            )
        {
            _factory = factory;
            foreach (var mapper in mappers)
            {
                mapper.Initialize(this);
            }
        }

        public Dictionary<string, string> Get()
        {
            return _dictionary;
        }

        public void Add<T>()
        {
            var assemblyQualifiedTypeName = typeof (T).AssemblyQualifiedName;
            var typeName = typeof (T).FullName;
            _dictionary[typeName] = _factory
                .CreateExampleJsonObjectOfType(assemblyQualifiedTypeName);
        }

        public object Map(object @object, string messageTypeName)
        {
            throw new NotImplementedException();
        }
    }
}