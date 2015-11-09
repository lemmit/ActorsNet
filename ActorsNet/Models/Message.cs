using ActorsNet.Models.Interfaces;

namespace ActorsNet.Models
{
    public class Message : IMessage
    {
        public Message(string messageTypeName, object messageData, string guid)
        {
            MessageTypeName = messageTypeName;
            MessageData = messageData;
            Guid = guid;
        }

        public string MessageTypeName { get; }
        public object MessageData { get; }
        public string Guid { get; }
        public bool HasGuid => !string.IsNullOrEmpty(Guid);
    }
}