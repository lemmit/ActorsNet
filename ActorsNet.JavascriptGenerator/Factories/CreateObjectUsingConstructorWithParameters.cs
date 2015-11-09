using System;
using ActorsNet.JavascriptGenerator.Helper;

namespace ActorsNet.JavascriptGenerator.Factories
{
    internal class CreateObjectUsingConstructorWithParameters : CreateObjectOfType
    {
        private readonly CreateObjectOfType _chain;

        public CreateObjectUsingConstructorWithParameters(CreateObjectOfType firstHandlerFromChain)
        {
            _chain = firstHandlerFromChain;
        }

        protected override bool CanCreate(Type type)
        {
            return true;
        }

        protected override object CreateObject(Type type)
        {
            var constructor = ReflectionHelper.ConstructorWithMinimalNumberOfParameters(type);
            var constructorParams = constructor.GetParameters();
            var numberOfParams = constructorParams.Length;
            var parameters = new object[numberOfParams];
            for (var i = 0; i < numberOfParams; i++)
            {
                var param = constructorParams[i];
                var paramType = param.ParameterType;
                if (param.HasDefaultValue)
                {
                    parameters[i] = param.DefaultValue;
                    continue;
                }
                parameters[i] = _chain.Create(paramType);
            }
            return Activator.CreateInstance(type, parameters);
        }
    }
}