namespace ActorsNet.Initializers.Interfaces
{
    public interface INamedMapperInitializer : IMapperInitializer
    {
        string Name { get; }
    }
}