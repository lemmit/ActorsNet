using ActorsNet.Initializers.Interfaces;
using ActorsNet.JavascriptGenerator.Controllers;
using ActorsNet.JavascriptGenerator.Factories;
using ActorsNet.JavascriptGenerator.Factories.Interfaces;
using ActorsNet.JavascriptGenerator.Providers;
using ActorsNet.JavascriptGenerator.Providers.Interfaces;
using Autofac;

namespace ActorsNet.JavascriptGenerator.Autofac
{
    /// <summary>
    /// ActorsNet.JavascriptGenerator module initializer
    /// </summary>
    public class AutofacActorsNetJavascriptGeneratorInitializer
    {
        /// <summary>
        /// Register ActorsNet.JavascriptGenerator module types in IoC container.
        /// </summary>
        /// <param name="builder">Autofac ContainerBuilder.</param>
        /// <param name="mapperInitializer">The mapper initializer - users definition of actor messages.</param>
        public void RegisterTypes(ContainerBuilder builder,
            IMapperInitializer mapperInitializer)
        {
            builder.RegisterInstance(mapperInitializer)
                .As<IMapperInitializer>();
            builder.RegisterType<JsonFromMessageProvider>()
                .As<IJsonStringOfMessageProvider>();
            builder.RegisterType<JsonStringifiedObjectFactory>()
                .As<IJsonStringifiedObjectFactory>();
            builder.RegisterType<NamesBySystemProvider>()
                .As<INamesBySystemProvider>();
            builder.RegisterType<ActorNetController>().InstancePerRequest();
        }
    }
}