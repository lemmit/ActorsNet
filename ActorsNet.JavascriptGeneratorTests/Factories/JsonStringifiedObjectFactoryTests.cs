using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActorsNet.JavascriptGenerator.Factories.Tests
{
    [TestClass]
    public class JsonStringifiedObjectFactoryTests
    {
        private JsonStringifiedObjectFactory _jsonFactory = new JsonStringifiedObjectFactory();

        private string GetName<T>()
        {
            return typeof (T).AssemblyQualifiedName;
        }

        private string CreateStringifiedObject<T>()
        {
            var name = GetName<T>();
            return _jsonFactory.CreateExampleJsonObjectOfType(name);
        }

        [TestMethod]
        public void CreateExampleJsonObjectOfBuiltinType()
        {
            const string stringifiedObject = "0";
            var strObj = CreateStringifiedObject<int>();
            Assert.AreEqual(stringifiedObject, strObj);
        }

        public class TypeWithParameterlessConstructor
        {
            public string Field { get; private set; }
            public TypeWithParameterlessConstructor()
            {
                //a lil' bit weird case, serializers ignore those fields...
                Field = "test";
            }
        }

        [TestMethod]
        public void CreateExampleJsonObjectUsingParameterlessConstructorTest()
        {
            const string stringifiedObject = "{}";
            var strObj = CreateStringifiedObject<TypeWithParameterlessConstructor>();
            Assert.AreEqual(stringifiedObject, strObj);
        }

        public class TypeWithConstructorWithParameters
        {
            public string Field { get; private set; }
            public int NumberField { get; private set; }
            public TypeWithConstructorWithParameters(string field, int number)
            {
                Field = field;
                NumberField = number;
            }
        }

        [TestMethod]
        public void CreateExampleJsonObjectUsingConstructorWithParametersTest()
        {
            const string stringifiedObject = "{ Field: null, NumberField: 0}";
            var strObj = CreateStringifiedObject<TypeWithConstructorWithParameters>();
            Assert.AreEqual(stringifiedObject, strObj);
        }

        public interface InterfaceType
        {
            string Field { get; }
        }

        [TestMethod]
        public void CreateExampleJsonObjectOfInterface()
        {
            //should be exception and return {}
            const string stringifiedObject = "{}";
            var strObj = CreateStringifiedObject<InterfaceType>();
            Assert.AreEqual(stringifiedObject, strObj);
        }

        public abstract class AbstractType
        {
            string Field { get; }
        }

        [TestMethod]
        public void CreateExampleJsonObjectOfAbstractClass()
        {
            //should be exception inside and return {}
            const string stringifiedObject = "{}";
            var strObj = CreateStringifiedObject<AbstractType>();
            Assert.AreEqual(stringifiedObject, strObj);
        }

        public enum IntBasedEnumType : int
        {
            Default = 0,
            One = 1,
            Two = 2,
            Three = 3
        }
        
        [TestMethod]
        public void CreateExampleJsonObjectOfEnum()
        {
            const string stringifiedObject = "0"; // or value of the enum base type?
            var strObj = CreateStringifiedObject<IntBasedEnumType>();
            Assert.AreEqual(stringifiedObject, strObj);


        }
    }
}