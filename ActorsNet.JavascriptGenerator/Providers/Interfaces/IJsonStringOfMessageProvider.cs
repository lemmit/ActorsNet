using System.Collections.Generic;

namespace ActorsNet.JavascriptGenerator.Providers.Interfaces
{
    public interface IJsonStringOfMessageProvider
    {
        Dictionary<string, string> Get();
    }
}