using ActorsNet.Initializers.Interfaces;
using ActorsNet.Services.Interfaces;
using ActorsNet.Web.Messages;

namespace ActorsNet.Web.Initializers
{
    public class MySystemMapperInitializer : INamedMapperInitializer
    {
        public MySystemMapperInitializer(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public void Initialize(IMapper messageMapper)
        {
            messageMapper.Add<Greet>();
            messageMapper.Add<Echo>();
        }
    }
}