using System;
using System.Linq;
using ActorsNet.SignalR.Services.Interfaces;
using Newtonsoft.Json.Linq;

namespace ActorsNet.SignalR.Services
{
    /// <summary>
    /// Maps jobject object to object of type T using its type name
    /// </summary>
    public interface ITypeFinder
    {
        Type FindType(string typeName, string assemblyName = null);
    }

    public class AppDomainWideTypeFinder : ITypeFinder
    {
        public Type FindType(string typeName, string assemblyName = null)
        {
            if (string.IsNullOrEmpty(typeName))
                throw new ArgumentNullException(nameof(typeName));

            var assemblies = AppDomain
                .CurrentDomain
                .GetAssemblies();

            if (assemblyName != null)
            {
                assemblies = assemblies
                    .Where(assembly => assembly.FullName.Contains(assemblyName))
                    .ToArray();
            }

            var foundType = assemblies
                .Select(assembly => assembly.GetType(typeName))
                .First(type => type != null);

            return foundType;
        }
    }

    public class JObjectToStronglyTypedObjectMapper : IMessageMapper
    {
        private readonly ITypeFinder _typeFinder;

        public JObjectToStronglyTypedObjectMapper(ITypeFinder typeFinder = null)
        {
            _typeFinder = typeFinder ?? new AppDomainWideTypeFinder();
        }

        public object Map(JObject message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            var fullType = GetTypeOfObject(message);
            var typeName = GetTypeName(fullType);
            var assemblyName = GetAssemblyName(fullType);
            var foundType = _typeFinder.FindType(typeName, assemblyName);
            var msgBack = CastParsedMessageToItsStrongType(message, foundType);

            return msgBack;
        }

        private object CastParsedMessageToItsStrongType(JObject parsedMessage, Type foundType)
        {
            var msgBack = parsedMessage.ToObject(foundType);
            return msgBack;
        }

        private string GetAssemblyName(string fullType)
        {
            try
            {
                return fullType.Split(',')[1].Trim();
            }
            catch (IndexOutOfRangeException) { }
            return null;
        }

        private string GetTypeName(string fullType)
        {
            return fullType.Split(',')[0];
        }

        private string GetTypeOfObject(JObject parsedMessage)
        {

            var fullType = (string)parsedMessage["$type"];
            return fullType;
        }
    }
}