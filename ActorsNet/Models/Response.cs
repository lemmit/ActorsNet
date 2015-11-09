using ActorsNet.Models.Interfaces;

namespace ActorsNet.Models
{
    public class Response : IResponse
    {
        public string ReplyTo { get; set; }
        public object Message { get; set; }
        public int ErrorCode { get; set; }
    }
}