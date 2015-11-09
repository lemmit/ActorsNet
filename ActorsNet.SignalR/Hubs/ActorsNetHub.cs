﻿using System;
using ActorsNet.Infrastructure;
using ActorsNet.Models;
using ActorsNet.Services;
using ActorsNet.Services.Interfaces;
using ActorsNet.SignalR.Exceptions;
using Microsoft.AspNet.SignalR;

namespace ActorsNet.SignalR.Hubs
{
    public class ActorsNetHub : Hub
    {
        private readonly ActorSystemResolver _actorSystemServiceResolver;
        private readonly IMessageMapper _messageMapper;
        private readonly TimeSpan standardTimeOut = TimeSpan.FromSeconds(20);

        public ActorsNetHub(ActorSystemResolver actorSystemServiceResolver, IMessageMapper messageMapper)
        {
            _actorSystemServiceResolver = actorSystemServiceResolver;
            _messageMapper = messageMapper;
        }

        private ActorSystem GetActorSystem(string systemName)
        {
            return _actorSystemServiceResolver.Resolve(systemName);
        }

        public bool ActorExists(string systemName, string actorPath)
        {
            return GetActorSystem(systemName).ActorExists(actorPath);
        }

        public void Send(string systemName, string actorPath, Message message)
        {
            try
            {
                var messageToActor = _messageMapper.Map(
                    message.MessageData,
                    message.MessageTypeName
                    );
                GetActorSystem(systemName).TellActor(actorPath, messageToActor);
            }
            catch (ActorNotFoundException e)
            {
                SendNotFoundMessageToClient(message.Guid);
            }
            catch (Exception e)
            {
            }
        }

        public void Ask(string systemName, string actorPath, Message message)
        {
            Ask(systemName, actorPath, message, standardTimeOut);
        }

        public void Ask(string systemName, string actorPath, Message message, TimeSpan timeOut)
        {
            try
            {
                var messageToActor = _messageMapper.Map(
                    message.MessageData,
                    message.MessageTypeName
                    );
                GetActorSystem(systemName).AskActor(actorPath, messageToActor, response =>
                {
                    var resp = new Response
                    {
                        ReplyTo = message.Guid,
                        ErrorCode = 0,
                        Message = response
                    };
                    Clients.Caller.actorsNetResponse(resp);
                }, timeOut);
            }
            catch (ActorNotFoundException e)
            {
                SendNotFoundMessageToClient(message.Guid);
            }
            catch (Exception e)
            {
            }
        }

        private void SendNotFoundMessageToClient(string guid)
        {
            if (string.IsNullOrEmpty(guid))
            {
                return;
            }

            var msg = new Response
            {
                ErrorCode = 404,
                ReplyTo = guid
            };
            Clients.Caller.actorsNetResponse(msg);
        }
    }
}