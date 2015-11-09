namespace ActorsNet.Models.Interfaces
{
    public interface IResponse
    {
        string ReplyTo { get; }
        object Message { get; }
        int ErrorCode { get; }
    }
}