using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public interface IParentWorldQuery : IWorldQuery;


public struct ParentQuery<Q> : IParentWorldQuery
    where Q : struct, IEntityQuery
{

    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public Q Query => new() { World = MainWorld, CallingProcessor = CallingProcessor };
    public readonly World MainWorld => World.GetResource<World>();
}