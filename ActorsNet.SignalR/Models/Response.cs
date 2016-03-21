namespace ActorsNet.SignalR.Models
{
    public class Response
    {
        public int ErrorCode { get; set; }
        public string ReplyToGuid { get; set; }
        public object Payload { get; set; }
    }
}