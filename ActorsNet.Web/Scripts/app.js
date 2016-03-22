var exampleWithoutGenerator = function (askButtonSelector, destinationSelector, messageSelector) {
    var actorSystem = new ActorsNetSystem("MySystem");
    console.log("ActorSystem Created!");

    actorSystem
        .open()
        .then(function() {
            console.log("System opened.");
            /* Send test */
            /*var greetActor = actorSystem.actorFor("/greeting");
            var msg = actorSystem.createMessage("ActorsNet.Web.Messages.Greet");
            msg.setMessageData({ Who: "SignalR send test - that's who!" });
            greetActor
                .send(msg);*/

            /* Ask test */

            var getMessage = function() {
                return $(messageSelector).val();
            };
            $(askButtonSelector).on('click', function (e) {
                console.log("Submit sent");
                e.preventDefault();
                var echoActor = actorSystem.actorFor("/echo");
                var echoMsg = actorSystem.createMessage("ActorsNet.Web.Messages.Echo");
                echoMsg.setMessageData({ Message: getMessage() });
                echoActor
                    .ask(echoMsg)
                    .then(function (response) {
                        $(destinationSelector).append("<li>" + response.Message + "</li>");
                        console.log("Response received by sender[ASK]!");
                        console.log(response);
                    });
            });
        });
};

var exampleUsingJsGenerator = function(askButtonSelector, destinationSelector, messageSelector) {
    var mySystem = ActorsNet.MySystem.create();
    console.log("ActorSystem[2] Created!");
    mySystem
        .open()
        .then(function() {
            console.log("System[2] opened.");
            /*var greetActor = mySystem.actorFor("/greeting");
            var greetMessage = ActorsNet.MySystem.ActorsNet_Web_Messages_Greet.create();
            greetMessage.setMessageData({ Who: "That's me" });
            greetActor
                .send(greetMessage);*/
            var getMessage = function () {
                return $(messageSelector).val();
            };
            $(askButtonSelector).on('click', function (e) {
                console.log("Submit sent");
                e.preventDefault();
                var echoActor = mySystem.actorFor("/echo");
                var echoMessage = ActorsNet.MySystem.ActorsNet_Web_Messages_Echo.create();
                echoMessage.setMessageData({ Message: getMessage() });
                echoActor
                    .ask(echoMessage)
                    .then(function (response) {
                        $(destinationSelector).append("<li>" + response.Message + "</li>");
                        console.log("Response received by sender[ASK]!");
                        console.log(response);
                    });
            });
        });
};

//exampleWithoutGenerator('input#askButton', 'ul#responses', 'input#echoMessage');
exampleUsingJsGenerator('input#askButton', 'ul#responses', 'input#echoMessage');