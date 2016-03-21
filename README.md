ActorsNet v0.1.0
---------

Asp.Net MVC + SignalR meets Actors!

Call the sever-side actors within your javascript. 
Initialize your actor system and done!

---------
## Modules description

### ActorsNet
Abstraction layer over actor system or framework.

### ActorsNet.AkkaNet
Concrete implementation for ActorsNet classes using Akka.Net

### ActorsNet.SignalR
Module that allows async calls between server and client.
Contains script for creating ActorSystem proxy on the client-side and call server-side actors 

### ActorsNet.JavascriptGenerator
Generates factories for creating strongly typed messages (possibility to save generated js and use it for code completion instead of writing message type names by hand).

### ActorsNet.Web
Example application, contains actor system creation code, initializers and example calls.

### ActorsNet.{SignalR, JavaScriptGenerator}.Autofac
Projects that contains initializer classes that can be used with Autofac IoC.

## Examples

### Server-side

#### Define actors and messages
```language-csharp
    public class EchoActor : ReceiveActor
    {
        public EchoActor()
        {
            Receive<Echo>(echo =>Sender.Tell(echo, Self));
        }
    }

	public class Echo
    {
        public Echo(string message)
        {
            Message = message;
        }
		public string Message { get; private set; }
    }
```
#### Create actor system, and initialize it in the container

	builder.RegisterActorSystem(actorSystem);	

### Example without ActorsNet.JavascriptGenerator
```language-javascript
	(function() {
    var actorSystem = new ActorsNetSystem("MySystem");
    actorSystem
        .open()
        .then(function() { //system ready
            var echoActor = actorSystem.actorFor("/echo");
            var echoMsg = actorSystem.createMessage("ActorsNet.Web.Messages.Echo");
            echoMsg.setMessageData({ Message: "SignalR ask test" });
			//send message
            echoActor.send(echoMsg);
            //or ask and wait for response 
            echoActor
                .ask(echoMsg)
                .then(function(response) {
                    alert(response);
                });
        });
	})();
```
### Example with JavascriptGenerator
```language-javascript
	(function() {
	    var mySystem = ActorsNet.MySystem.create();
	    mySystem
	        .open()
	        .then(function() {
	            var echoActor = mySystem.actorFor("/echo");
	            var echoMessage = ActorsNet.MySystem.ActorsNet_Web_Messages_Echo.create();
	            echoMessage.setMessageData({ Message: "Eccchoooo!" });
	            //send message
				echoActor.send(echoMessage);
				//or ask and wait for response
	            echoActor
	                .ask(echoMessage)
	                .then(function(response) {
	                    alert(response);
	                });
	        });
	})();
```

### TODOs
- Add more tests
- Implement and test different Akka.Net ActorFinder strategies (akka.tcp/cluster)
- Integration with client side actors library (https://github.com/mental/webactors ?) to allow communication between JS actors
- Implementation of a server side router actor to allow calls from server to client-side declared actors (Actorish-WebWorkers)
- Different implementation of ActorSystem (Microsoft Orleans)