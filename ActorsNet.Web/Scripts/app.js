var exampleOne = function() {
    var actorSystem = new ActorsNetSystem("MySystem");
    console.log("ActorSystem Created!");

    actorSystem
        .open()
        .then(function() {
            console.log("System opened.");
            /* Send test */
            var greetActor = actorSystem.actorFor("/greeting");
            var msg = actorSystem.createMessage("ActorsNet.Web.Messages.Greet");
            msg.setMessageData({ Who: "SignalR send test - that's who!" });
            greetActor
                .send(msg);

            /* Ask test */
            var echoActor = actorSystem.actorFor("/echo");
            var echoMsg = actorSystem.createMessage("ActorsNet.Web.Messages.Echo");
            echoMsg.setMessageData({ Message: "SignalR ask test" });
            echoActor
                .ask(echoMsg)
                .then(function(response) {
                    console.log("Response received by sender[ASK]!");
                    console.log(response);
                });
        });
};

var exampleUsingJsGenerator = function() {
    var mySystem = ActorsNet.MySystem.create();
    console.log("ActorSystem[2] Created!");
    mySystem
        .open()
        .then(function() {
            console.log("System[2] opened.");
            var greetActor = mySystem.actorFor("/greeting");
            var greetMessage = ActorsNet.MySystem.ActorsNet_AkkaNet_Models_Greet.create();
            greetMessage.setMessageData({ Who: "That's me" });
            greetActor
                .send(greetMessage);

            var echoActor = mySystem.actorFor("/echo");
            var echoMessage = ActorsNet.MySystem.ActorsNet_AkkaNet_Models_Echo.create();
            echoMessage.setMessageData({ Message: "Eccchoooo!" });
            echoActor
                .ask(echoMessage)
                .then(function(response) {
                    console.log("Response received by sender[ASK]!");
                    console.log(response);
                });
        });
};

exampleOne();
//exampleUsingJsGenerator();