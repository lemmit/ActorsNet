using System.Collections.Generic;

namespace ActorsNet.JavascriptGenerator.Providers.Interfaces
{
    public interface IMessageNamesBySystemProvider
    {
        IDictionary<string, IList<string>> Get();
    }
}