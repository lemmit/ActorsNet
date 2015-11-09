using ActorsNet.Services.Interfaces;

namespace ActorsNet.Initializers.Interfaces
{
    public interface IMapperInitializer
    {
        void Initialize(IMapper messageMapper);
    }
}