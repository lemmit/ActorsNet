using System;
using System.Collections.Generic;

namespace ActorsNet.JavascriptGenerator.Factories
{
    internal class CreateObjectOfBuiltInType : CreateObjectOfType
    {
        private static readonly IDictionary<Type, object> PredefinedObjectsValues = new Dictionary<Type, object>
        {
            {typeof (int), 0},
            {typeof (uint), 0},
            {typeof (short), 0},
            {typeof (ushort), 0},
            {typeof (long), 0},
            {typeof (ulong), 0},
            {typeof (string), null},
            {typeof (char), 0},
            {typeof (bool), false},
            {typeof (sbyte), 0},
            {typeof (byte), 0},
            {typeof (float), 0.0f},
            {typeof (double), 0.0f},
            {typeof (decimal), 0m}
        };

        protected override bool CanCreate(Type type)
        {
            return PredefinedObjectsValues.ContainsKey(type);
        }

        protected override object CreateObject(Type type)
        {
            return PredefinedObjectsValues[type];
        }
    }
}