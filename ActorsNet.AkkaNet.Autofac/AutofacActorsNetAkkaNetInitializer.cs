using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActorsNet.AkkaNet.Infrastructure;
using ActorsNet.Infrastructure.Interfaces;

namespace ActorsNet.AkkaNet.Autofac
{
    public class AutofacActorsNetAkkaNetInitializer
    {
        public void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<Actor>().As<IActor>();
            builder.RegisterType<ActorFinder>().As<IActorFinder>();
        }
    }
}
