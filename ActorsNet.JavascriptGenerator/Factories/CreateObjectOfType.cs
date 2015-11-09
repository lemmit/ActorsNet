using System;

namespace ActorsNet.JavascriptGenerator.Factories
{
    internal abstract class CreateObjectOfType
    {
        public CreateObjectOfType Next { private get; set; }

        protected abstract bool CanCreate(Type type);
        protected abstract object CreateObject(Type type);

        public object Create(Type type)
        {
            if (CanCreate(type))
            {
                return CreateObject(type);
            }
            return Next?.Create(type);
        }
    }
}