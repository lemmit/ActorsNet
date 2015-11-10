using System;
using ActorsNet.JavascriptGenerator.Factories.Interfaces;
using ActorsNet.JavascriptGenerator.Helper;
using Newtonsoft.Json;

namespace ActorsNet.JavascriptGenerator.Factories
{
    /// <summary>
    /// Factory that from given assembly qualified type name creates object and stringifies it to JSON
    /// </summary>
    /// <remarks>
    /// From given type T
    /// e.g.:
    /// public class Message
    /// {
    ///     public string A { get; private set; }
    ///     ...
    ///     public Message(string a, string b, string c...)
    ///     {
    ///     ...
    ///     }
    /// }
    /// creates JSON string:
    /// {
    ///     "a" : ...,
    ///     "b" : ...,
    ///     "c" : ...,
    ///     ...
    /// }
    /// 
    /// It uses JObject.FromObject method
    /// (beware of default constructors!)
    /// PROTOTYPE!
    /// </remarks>
    public class JsonStringifiedObjectFactory : IJsonStringifiedObjectFactory
    {
        public string CreateExampleJsonObjectOfType(string qualifiedAssemblyName)
        {
            var serializedObj = "{}";
            try
            {
                var type = CheckType(qualifiedAssemblyName);
                var obj = CreateExampleObjectFromTypeName(type);
                serializedObj = SerializeObjectToString(obj);
            }
            catch (Exception e)
            {
                //PROTOTYPE.
            }
            return serializedObj;
        }

        private Type CheckType(string qualifiedAssemblyName)
        {
            var foundType = Type.GetType(qualifiedAssemblyName);
            if (foundType == null)
            {
                throw new ArgumentException("Type " + qualifiedAssemblyName + " does not exist.");
            }
            if (ReflectionHelper.MinimalNumberOfParametersFromType(foundType) <= 0)
            {
                //generating json object of type that have parameterless constructor
                //Json serializer will return {}....
                //throw new ArgumentException("Type " + qualifiedAssemblyName + " has no constructor with parameter [json{}].");
            }
            return foundType;
        }

        private object CreateExampleObjectFromTypeName(Type type, string offset = "")
        {
            var chain = CreateChain();
            return chain.Create(type);
        }

        private static CreateObjectOfType CreateChain()
        {
            CreateObjectOfType chain = new CreateObjectOfBuiltInType();
            var second = new CreateObjectUsingParameterlessContructor();
            var third = new CreateObjectUsingConstructorWithParameters(chain);
            chain.Next = second;
            second.Next = third;
            return chain;
        }

        private string SerializeObjectToString(object obj)
        {
            var exampleJsonObjectString = JsonConvert.SerializeObject(obj);
            return exampleJsonObjectString;
        }
    }
}