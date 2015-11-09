using System.Diagnostics;
using ActorsNet.Web.Messages;
using Akka.Actor;

namespace ActorsNet.Web.Actors
{
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