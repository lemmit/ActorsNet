ActorsNet v0.1.0
---------

Asp.Net MVC + SignalR meets Actors!
Call the actors from your javascript. All you have to do is to declare used messages types.

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
	
	//Initialization class for message mapper used by SignalR hub to cast messages to types understandable by actor system, and used as initializer for JavascriptGenerator module
	public class MySystemMapperInitializer : INamedMapperInitializer
    {
        public MySystemMapperInitializer(string name)
        {
            Name = name;
        }

        public string Name { get; }
		
        public void Initialize(IMapper messageMapper)
        {
            messageMapper.Add<Greet>();
            messageMapper.Add<Echo>();
        }
    }

### Example without ActorsNet.JavascriptGenerator
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

### Example with JavascriptGenerator
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


### TODOs
- Integration with client side actors library (https://github.com/mental/webactors ?) to allow communication between JS actors
- Implementation of a server side router actor to allow calls from server to client-side declared actors (Actorish-WebWorkers)
- Different implementation of ActorSystem (Microsoft Orleans)