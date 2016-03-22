using System.Web.Mvc;
using ActorsNet.AkkaNet.Infrastructure;
using ActorsNet.SignalR.Autofac;
using ActorsNet.Web.Actors;
using ActorsNet.Web.Logging;
using Akka.Actor;
using Autofac;
using Microsoft.AspNet.SignalR;
using Owin;
using ActorSystem = ActorsNet.Infrastructure.ActorSystem;
using AkkaActorSystem = Akka.Actor.ActorSystem;
using AutofacDependencyResolver = Autofac.Integration.Mvc.AutofacDependencyResolver;
using ActorsNet.JavascriptGenerator.Autofac;
using ActorsNet.JavascriptGenerator.Providers;
using ActorsNet.Web.Messages;

namespace ActorsNet.Web
{
    public static class StartupExt
    {
       public static void RegisterActorSystem(this ContainerBuilder builder, AkkaActorSystem akkaActorSystem)
        {
            var actorFinder = new ActorFinder(akkaActorSystem);
            var actorSystem = new ActorSystem(akkaActorSystem.Name, actorFinder);
            builder.RegisterInstance<ActorSystem>(actorSystem);
        }

        public static void RegisterLogger(this ContainerBuilder builder)
        {
            builder.RegisterModule<LogRequestAutofacModule>();
        }

        public static void SetIoCContainerForMVC(this IContainer container)
        {
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }

        public static void SetIoCContainerForSignalR(this IContainer container)
        {
            var signalRResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            GlobalHost.DependencyResolver = signalRResolver;
        }
    }
    

    public class Startup
    {
        const string ActorSystemName = "MySystem";
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.RegisterLogger();

            //initialize ActorsNet modules
            builder.RegisterJavascriptGenerator(new MessagesBySystemNameProvider()
                                                        .Add<Echo>(ActorSystemName)
                                                        .Add<Greet>(ActorSystemName));
            builder.RegisterSignalRRouterHub();

            var actorSystem = CreateAkkaActorSystem();
            builder.RegisterActorSystem(actorSystem);

            var container = builder.Build();
            container.SetIoCContainerForMVC();
            container.SetIoCContainerForSignalR();

            app.MapSignalR();
        }

        private static AkkaActorSystem CreateAkkaActorSystem()
        {
            var actorSystem = AkkaActorSystem.Create(ActorSystemName);
            actorSystem.ActorOf<GreetingActor>("greeting");
            actorSystem.ActorOf<EchoActor>("echo");
            return actorSystem;
        }
        
    }
}