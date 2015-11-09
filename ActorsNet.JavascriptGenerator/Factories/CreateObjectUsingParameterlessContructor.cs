using System;
using ActorsNet.JavascriptGenerator.Helper;

namespace ActorsNet.JavascriptGenerator.Factories
{
    internal class CreateObjectUsingParameterlessContructor : CreateObjectOfType
    {
        protected override bool CanCreate(Type type)
        {
            return ReflectionHelper.MinimalNumberOfParametersFromType(type) <= 0;
        }

        protected override object CreateObject(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}