namespace ActorsNet.Models.Interfaces
{
    public interface IMessage
    {
        string MessageTypeName { get; }
        object MessageData { get; }
        string Guid { get; }
        bool HasGuid { get; }
    }
}