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
    public class AutofacActorsNetSignalRInitializer
    {
        /// <summary>
        /// Register ActorsNet.SignalR module types in IoC container.
        /// </summary>
        /// <param name="builder">Autofac ContainerBuilder.</param>
        /// <param name="mapperInitializer">The mapper initializer - users definition of actor messages.</param>
        public void RegisterTypes(ContainerBuilder builder,
            INamedMapperInitializer mapperInitializer)
        {
            builder.RegisterInstance(mapperInitializer)
                .As<INamedMapperInitializer>();
            builder.RegisterType<JObjectToActorMessageMapper>()
                .As<IMessageMapper>();
            builder.RegisterType<ActorSystemResolver>();
            builder.RegisterType<ActorsNetHub>().ExternallyOwned();
        }
    }
}