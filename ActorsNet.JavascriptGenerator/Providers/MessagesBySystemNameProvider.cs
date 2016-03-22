using System.Collections.Generic;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;

namespace ActorsNet.JavascriptGenerator.Providers
{
    public class MessagesBySystemNameProvider : IMessageNamesBySystemProvider
    {
        readonly IDictionary<string, IList<string>> _mappings
            = new Dictionary<string, IList<string>>();

        public MessagesBySystemNameProvider Add<T>(string systemName)
        {
            IList<string> messagesList;
            if (!_mappings.TryGetValue(systemName, out messagesList))
            {
                messagesList = new List<string>();
            }
            messagesList.Add(typeof(T).FullName);
            _mappings[systemName] = messagesList;
            return this;
        }
        public IDictionary<string, IList<string>> Get()
        {
            return _mappings;
        }
    }
}