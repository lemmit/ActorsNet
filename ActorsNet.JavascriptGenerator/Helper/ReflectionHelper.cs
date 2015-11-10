using System;
using System.Linq;
using System.Reflection;

namespace ActorsNet.JavascriptGenerator.Helper
{
    internal static class ReflectionHelper
    {
        public static ConstructorInfo ConstructorWithMinimalNumberOfParameters(Type type)
        {
            var constructors = type.GetConstructors();
            var sortedConstructors = constructors.OrderBy(constructor => constructor.GetParameters().Length);
            var c = sortedConstructors.First();
            return c;
        }

        public static int MinimalNumberOfParametersFromType(Type type)
        {
            if (!type.IsClass)
            {
                return 0;
            }

            var constructor = ConstructorWithMinimalNumberOfParameters(type);
            var minimalNumberOfParameters = constructor.GetParameters().Length;
            return minimalNumberOfParameters;
        }
    }
}