(function() {
    var mySystem = AkkaNetJs.MySystem.create();
    mySystem.open(function() {
        console.log("AkkaSystem MySystem open!");

        var greetActor = mySystem.actorFor("/greeting");
        var greetMessage = AkkaNetJs.MySystem.Greet.create();
        greetMessage.setData({ Who: "That's me" });
        greetActor.send(greetMessage);

        var echoActor = mySystem.actorFor("/echo");
        var echoMessage = AkkaNetJs.MySystem.Echo.create();
        echoMessage.setData({ Message: "Eccchoooo!" });
        echoActor.send(echoMessage);

        //echoActor.ask(echoMessage)
    });

})();