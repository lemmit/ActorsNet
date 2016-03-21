namespace ActorsNet.SignalR.Mappers.Interfaces
{
    public interface IMessageMapper
    {
        object Map(object messageData, string messageTypeName);
    }
}