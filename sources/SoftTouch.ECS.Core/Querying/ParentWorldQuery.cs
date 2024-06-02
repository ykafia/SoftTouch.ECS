using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Querying;


public interface IParentWorldQuery : IWorldQuery;


public struct ParentWorld : IParentWorldQuery
{
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public readonly World Parent => World.GetResource<World>();

    public static implicit operator World(ParentWorld pw) => pw.Parent;
}