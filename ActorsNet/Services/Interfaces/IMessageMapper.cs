namespace ActorsNet.Services.Interfaces
{
    public interface IMessageMapper : IMapper
    {
        //void AddMessagesImplementingInterface<T>();
        //void AddMessagesWithBaseClass<T>();
        object Map(object @object, string messageTypeName);
    }
}