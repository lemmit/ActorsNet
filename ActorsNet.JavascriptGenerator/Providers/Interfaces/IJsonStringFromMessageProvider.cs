using System.Collections.Generic;

namespace ActorsNet.JavascriptGenerator.Providers.Interfaces
{
    public interface IJsonStringFromMessageProvider
    {
        Dictionary<string, string> Get();
    }
}