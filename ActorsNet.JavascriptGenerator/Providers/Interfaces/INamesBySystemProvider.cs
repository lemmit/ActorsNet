using System.Collections.Generic;

namespace ActorsNet.JavascriptGenerator.Providers.Interfaces
{
    public interface INamesBySystemProvider
    {
        Dictionary<string, List<string>> Get();
    }
}