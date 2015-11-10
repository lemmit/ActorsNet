using System;
using System.Runtime.Serialization;

namespace ActorsNet.Exceptions
{
    [Serializable]
    public class ActorNotFoundException : Exception
    {
        public ActorNotFoundException()
        {
        }

        public ActorNotFoundException(string message) : base(message)
        {
        }

        public ActorNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ActorNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}