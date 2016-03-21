using Newtonsoft.Json.Linq;

namespace ActorsNet.SignalR.Services.Interfaces
{
    public interface IMessageMapper
    {
        object Map(JObject message);
    }
}