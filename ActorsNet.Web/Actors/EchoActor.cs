using System.Diagnostics;
using ActorsNet.Web.Messages;
using Akka.Actor;

namespace ActorsNet.Web.Actors
{
    /// <summary>
    /// Returns Echo message to sender
    /// Implemented for testing purposes
    /// </summary>
    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            Receive<Echo>(echo =>
            {
                Debug.WriteLine("Echo: " + echo.Message
                                + "\nSender: " + Sender.Path);
                Sender.Tell(echo, Self);
            });
        }
    }
}