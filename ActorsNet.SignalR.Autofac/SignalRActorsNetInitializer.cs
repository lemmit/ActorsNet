using ActorsNet.Initializers.Interfaces;
using ActorsNet.Services;
using ActorsNet.Services.Interfaces;
using ActorsNet.SignalR.Hubs;
using ActorsNet.SignalR.Services;
using Autofac;

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
        }
    }
}