using System;
using System.Threading.Tasks;

namespace ActorsNet.Infrastructure.Interfaces
{
    public interface IActor
    {
        void Tell(object message);
        Task<T> Ask<T>(T message);
        Task<T> Ask<T>(T message, TimeSpan timeOut);
    }
}