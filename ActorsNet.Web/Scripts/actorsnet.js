var LoggingLevel = {
    OFF: 0,
    INFO: 1,
    WARN: 2,
    DEBUG: 3,
    ERROR: 4
};

var Logger = (function() {
    var logger = {
        _loggingLevel: 0,
        setLoggingLevel: function(level) {
            this._loggingLevel = level;
        },
        info: function(msg, obj) {
            if (this._loggingLevel >= LoggingLevel.INFO)
                console.log("Log: " + msg, obj);
        },
        debug: function(msg, obj) {
            if (this._loggingLevel >= LoggingLevel.DEBUG)
                console.log("Log: " + msg, obj);
        },
        warn: function(msg, obj) {
            if (this._loggingLevel >= LoggingLevel.WARN)
                console.log("Log: " + msg, obj);
        },
        error: function(msg, obj) {
            if (this._loggingLevel >= LoggingLevel.ERROR)
                console.log("Log: " + msg, obj);
        }
    };
    return logger;
})();

var ActorsNetSystem = function(systemName) {
    Logger.setLoggingLevel(LoggingLevel.ERROR);
    Logger.debug("Creating ActorsNet system: ", systemName);
    var system = {
        systemName: systemName,
        replyTo: {},
        hub: $.connection.actorsNetHub,
        actorFor: function(path) {
            Logger.debug("Creating actor: ", path);
            return {
                path: path,
                send: function(msg) { return system.send(this, msg); },
                ask: function(msg) { return system.ask(this, msg); }
            };
        }
    };
    system.createMessage = function(messageType) {
        Logger.debug("createMessage()", messageType);
        var Message = function(msgType) {
            this.payload = { '$type': msgType };
            this.guid = system.guid();
        };
        Message.prototype.setMessageData = function(data) {
            Object.assign(this.payload, data);
        };
        return new Message(messageType);
    };

    system.open = function() {
        var deferred = $.Deferred();
        $.connection.hub.start().done(function() {
            Logger.debug("system.open() resolved", null);
            deferred.resolve();
        });
        return deferred.promise();
    };

    system.hub.client.actorsNetResponse = function (msg) {
        Logger.debug("Response msg received: ", msg);
        var deffered = system.replyTo[msg.ReplyToGuid];
        if (deffered) {
            if (msg.ErrorCode === 0) {
                Logger.debug("Msg received[No Error]: ", msg);
                Logger.debug("Payload: " + msg.Payload);
                deffered.resolve(msg.Payload);
            } else {
                Logger.debug("Msg received[With Error]: ", msg);
                deffered.reject(msg.Payload);
            }
        } else {
            Logger.debug("No handler found for response message.");
        }
    };

    system.send = function(actor, msg) {
        Logger.debug("Msg send to actor: " + actor.path, msg);
        system.hub.server.send(system.systemName, actor.path, msg);
        var deferred = $.Deferred();
        if (msg.Guid) {
            system.replyTo[msg.guid] = deferred;
        } else deferred.resolve();
        return deferred.promise();
    };

    system.ask = function(actor, msg) {
        Logger.debug("Msg ask actor: " + actor.path, msg);
        var deferred = $.Deferred();
        system.replyTo[msg.guid] = deferred;
        system.hub.server.ask(system.systemName, actor.path, msg);
        return deferred.promise();
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