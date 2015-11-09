using System.Diagnostics;
using ActorsNet.Web.Messages;
using Akka.Actor;

namespace ActorsNet.Web.Actors
{
    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<Greet>(greet =>
            {
                Debug.WriteLine("Hello " + greet.Who
                                + "\nSender: " + Sender.Path);
                var greetBack = new Greet("Hello " + greet.Who);
                Sender.Tell(greetBack, Self);
            });
        }
    }
}