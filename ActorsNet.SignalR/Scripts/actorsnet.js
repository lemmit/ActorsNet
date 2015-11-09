var ActorsNetSystem = function(systemName) {
    var system = {
        systemName: systemName,
        replyTo: {},
        hub: $.connection.actorsNetHub,
        actorFor: function(path) {
            return {
                path: path,
                send: function(msg) { return system.send(this, msg); },
                ask: function(msg) { return system.ask(this, msg); }
            };
        }
    };

    system.createMessage = function(messageType) {
        var msg = {
            messageTypeName: messageType,
            MessageData: {},
            Guid: system.guid(),
            setData: function(data) {
                this.MessageData = data;
            }
        };
        return msg;
    };

    //TODO PROMISE
    system.open = function(doneCallback) {
        $.connection.hub.start().done(function() {
            console.log("Hub open");
            doneCallback();
        });
    };

    system.hub.client.actorsNetResponse = function(data) {
        console.log("Response msg received: ");
        console.log(data);
        var deffered = system.replyTo[data.replyTo];
        if (deffered) {
            $rootScope.$apply(function() {
                deffered.resolve(data.message);
            });
        }
    };

    system.send = function(actor, msg) {
        console.log("Message sent:");
        console.log(actor);
        console.log(msg);

        system.hub.server.send(system.systemName, actor.path, msg);
    };

    //TODO PROMISE
    system.ask = function(actor, msg) {
        console.log("Message asked:");
        console.log(actor);
        console.log(msg);
        /*var id = sys.uuid();
            deffered = $q.defer();
            sys.replyTo[id] = deffered;*/

        system.hub.server.ask(system.systemName, actor.path, msg);
        //return deffered.promise;
    };

    system.guid = function() {
        function s4() {
            return Math.floor((1 + Math.random()) * 0x10000)
                .toString(16)
                .substring(1);
        }

        return s4() + s4() + "-" + s4() + "-" + s4() + "-" +
            s4() + "-" + s4() + s4() + s4();
    };
    return system;
};