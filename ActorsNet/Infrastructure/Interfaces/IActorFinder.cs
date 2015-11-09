namespace ActorsNet.Infrastructure.Interfaces
{
    public interface IActorFinder
    {
        IActor FindActorByPath(string actorPath);
    }
}