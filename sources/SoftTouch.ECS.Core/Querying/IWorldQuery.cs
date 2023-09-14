using SoftTouch.ECS.Core;
using CommunityToolkit.HighPerformance.Buffers;


namespace SoftTouch.ECS.Core.Queries;


public interface IComponent
{

}

public interface IWorldQuery
{
    World World { get; }
}

public struct With<T>
    where T : struct, ValueTuple
{
    
}

public struct Without<T>
    where T : struct, IComponent
{

}