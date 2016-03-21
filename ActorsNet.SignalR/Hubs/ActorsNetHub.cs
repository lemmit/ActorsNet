using System;
using System.Diagnostics;
using ActorsNet.Exceptions;
using ActorsNet.Infrastructure;
using ActorsNet.Services;
using ActorsNet.SignalR.Models;
using ActorsNet.SignalR.Services.Interfaces;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json.Linq;

namespace ActorsNet.SignalR.Hubs
{
    /// <summary>
    /// Maps the messages from client to give actor system using system's name and message type name.
    /// </summary>
    public class ActorsNetHub : Hub
    {
        private readonly ActorSystemResolver _actorSystemResolver;
        private readonly IMessageMapper _messageMapper;
        private readonly TimeSpan _standardTimeOut = TimeSpan.FromSeconds(20);

        public ActorsNetHub(ActorSystemResolver actorSystemResolver, IMessageMapper messageMapper)
        {
            _actorSystemResolver = actorSystemResolver;
            _messageMapper = messageMapper;
        }

        private ActorSystem GetActorSystem(string systemName)
        {
            return _actorSystemResolver.Resolve(systemName);
        }

        public bool ActorExists(string systemName, string actorPath)
        {
            return GetActorSystem(systemName).ActorExists(actorPath);
        }

        public void Send(string systemName, string actorPath, JObject message)
        {
            try
            {
                var messageToActor = _messageMapper.Map((JObject)(message["payload"]));
                GetActorSystem(systemName).TellActor(actorPath, messageToActor);
            }
            catch (ActorNotFoundException)
            {
                SendNotFoundMessageToClient(ExtractGuid(message));
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
        }

        private static string ExtractGuid(JObject message)
        {
            return message["guid"].ToString();
        }

        public void Ask(string systemName, string actorPath, JObject message)
        {
            Ask(systemName, actorPath, message, _standardTimeOut);
        }

        public void Ask(string systemName, string actorPath, JObject message, TimeSpan timeOut)
        {
            try
            {
                var messageToActor = _messageMapper.Map((JObject)message["payload"]);
                GetActorSystem(systemName).AskActor(actorPath, messageToActor, response =>
                {
                    var resp = new Response
                    {
                        ReplyToGuid = ExtractGuid(message),
                        ErrorCode = 0,
                        Payload = response
                    };
                    Clients.Caller.actorsNetResponse(resp);
                }, timeOut);
            }
            catch (ActorNotFoundException)
            {
                SendNotFoundMessageToClient(ExtractGuid(message));
            }
            catch (Exception e)
            {
                Debug.Write(e);
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
                ReplyToGuid = guid,
                Payload = new object()
            };
            Clients.Caller.actorsNetResponse(msg);
        }
    }
}