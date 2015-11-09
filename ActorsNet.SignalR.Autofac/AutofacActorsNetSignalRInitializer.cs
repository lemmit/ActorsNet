using ActorsNet.Initializers.Interfaces;
using ActorsNet.Services;
using ActorsNet.Services.Interfaces;
using ActorsNet.SignalR.Hubs;
using ActorsNet.SignalR.Services;
using Autofac;

namespace ActorsNet.SignalR.Autofac
{
    public class AutofacActorsNetSignalRInitializer
    {
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