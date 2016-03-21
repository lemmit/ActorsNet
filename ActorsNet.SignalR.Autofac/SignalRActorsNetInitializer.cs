using System;
using ActorsNet.Services;
using ActorsNet.SignalR.Hubs;
using ActorsNet.SignalR.Services;
using ActorsNet.SignalR.Services.Interfaces;
using Autofac;
using Autofac.Integration.SignalR;

namespace ActorsNet.SignalR.Autofac
{
    /// <summary>
    /// ActorsNet.SignalR module initializer
    /// </summary>
    public static class SignalRActorsNetInitializer
    {
        /// <summary>
        /// Register ActorsNet.SignalR module types in IoC container.
        /// </summary>
        /// <param name="builder">Autofac ContainerBuilder.</param>
        public static void RegisterSignalRRouterHub(this ContainerBuilder builder)
        {
            builder.RegisterType<ActorSystemResolver>();
            builder.RegisterType<ActorsNetHub>().ExternallyOwned();
            builder.RegisterType<JObjectToStronglyTypedObjectMapper>().As<IMessageMapper>();
            builder.RegisterHubs(AppDomain.CurrentDomain.GetAssemblies()); 
        }
    }
}