using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Querying;


public interface IParentWorldQuery : IWorldQuery;


public struct Extract<Q> : IWorldQuery
    where Q : struct, IEntityQuery
{

    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public Q Query => new() { World = World, CallingProcessor = CallingProcessor };
    public readonly World MainWorld => World.GetResource<World>();
}