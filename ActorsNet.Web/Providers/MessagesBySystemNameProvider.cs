using System.Collections.Generic;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;
using ActorsNet.Web.Messages;

namespace ActorsNet.Web.Providers
{
    public class MessagesBySystemNameProvider : IMessageNamesBySystemProvider
    {
        public IDictionary<string, IList<string>> Get()
        {
            return new Dictionary<string, IList<string>>
            {
                { "MySystem", new []
                {
                    typeof(Greet).FullName,
                    typeof(Echo).FullName,
                }},
            };
        }
    }
}