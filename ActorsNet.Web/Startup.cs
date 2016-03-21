using System.Reflection;
using System.Web.Mvc;
using ActorsNet.AkkaNet.Infrastructure;
using ActorsNet.JavascriptGenerator.Autofac;
using ActorsNet.SignalR.Autofac;
using ActorsNet.Web.Actors;
using ActorsNet.Web.Initializers;
using ActorsNet.Web.Logging;
using Akka.Actor;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Owin;
using ActorSystem = ActorsNet.Infrastructure.ActorSystem;
using AkkaActorSystem = Akka.Actor.ActorSystem;
using AutofacDependencyResolver = Autofac.Integration.Mvc.AutofacDependencyResolver;

namespace ActorsNet.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // Creation of the system and message mappings
            // Here is the place where you should start playing with the library.
            var actorSystem = CreateAkkaActorSystem();
           
            var builder = new ContainerBuilder();
            //debug logger
            RegisterLogger(builder);

            //initialize ActorsNet modules
            builder.RegisterJavascriptGenerator();
            builder.RegisterSignalRRouterHub();
            
            RegisterActorSystem(builder, actorSystem);

            //register controllers and hubs from this assembly
            RegisterControllers(builder);
            RegisterHubs(builder);
            var container = builder.Build();
            //Use ioc container as default asp.net and signalrR dependency resolver
            SetIoCContainer(container);

            app.MapSignalR();
        }

        private static AkkaActorSystem CreateAkkaActorSystem()
        {
            const string actorSystemName = "MySystem";
            var actorSystem = AkkaActorSystem.Create(actorSystemName);
            actorSystem.ActorOf<GreetingActor>("greeting");
            actorSystem.ActorOf<EchoActor>("echo");
            return actorSystem;
        }

        private static void RegisterActorSystem(ContainerBuilder builder, AkkaActorSystem akkaActorSystem)
        {
            var actorFinder = new ActorFinder(akkaActorSystem);
            var actorSystem = new ActorSystem(akkaActorSystem.Name, actorFinder);
            builder.RegisterInstance<ActorSystem>(actorSystem);
        }

        private static void RegisterLogger(ContainerBuilder builder)
        {
            builder.RegisterModule<LogRequestAutofacModule>();
        }

        private static void SetIoCContainer(IContainer container)
        {
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);

            var signalRResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            GlobalHost.DependencyResolver = signalRResolver;
        }

        private static void RegisterHubs(ContainerBuilder builder)
        {
            builder.RegisterHubs(Assembly.GetExecutingAssembly());
        }

        private static void RegisterControllers(ContainerBuilder builder)
        {
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
        }
    }
}