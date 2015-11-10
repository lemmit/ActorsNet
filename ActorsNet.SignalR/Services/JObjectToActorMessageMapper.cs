using System;
using System.Collections.Generic;
using ActorsNet.Initializers.Interfaces;
using ActorsNet.Services.Interfaces;
using ActorsNet.SignalR.Exceptions;
using Newtonsoft.Json.Linq;

namespace ActorsNet.SignalR.Services
{
    /// <summary>
    /// Maps JObject to object of type T using its type name
    /// </summary>
    public class JObjectToActorMessageMapper : IMessageMapper
    {
        private readonly Dictionary<string, Func<JObject, object>> _dist;

        public JObjectToActorMessageMapper(IMapperInitializer mapperInitializer)
        {
            _dist = new Dictionary<string, Func<JObject, object>>();
            mapperInitializer.Initialize(this);
        }

        public void Add<T>()
        {
            _dist.Add(typeof (T).FullName, message => message.ToObject<T>());
        }

        public object Map(object @object, string messageTypeName)
        {
            var jobj = (JObject) @object;
            return Map(jobj, messageTypeName);
        }

        public void AddMessagesImplementingInterface<T>()
        {
            throw new NotImplementedException();
        }

        public void AddMessagesWithBaseClass<T>()
        {
            throw new NotImplementedException();
        }

        public object Map(JObject jObject, string messageTypeName)
        {
            try
            {
                var mappedObj = _dist[messageTypeName](jObject);
                return mappedObj;
            }
            catch (Exception e)
            {
                throw new MappingException("Error while mapping JObject to Actor Message", e);
            }
        }
    }
}