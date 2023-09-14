using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.CompilerServices;


namespace SoftTouch.ECS.Querying;



public interface IComponentQuery 
{
    public static abstract Type[] TypesRead { get; }
    public static abstract Type[] TypesWrite { get; }
    public static abstract Type[] TypesMayRead { get; }
    public static abstract Type[] TypesMayWrite { get; }
}
public interface IReadComponent : IComponentQuery { }
public interface IMayReadComponent : IReadComponent { }
public interface IWriteComponent : IComponentQuery { }
public interface IMayWriteComponent : IWriteComponent { }


public record Read<T>() : IReadComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = { typeof(T) };
    public static Type[] TypesWrite { get; } = Array.Empty<Type>();
    public static Type[] TypesMayRead { get; } = Array.Empty<Type>();
    public static Type[] TypesMayWrite { get; } = Array.Empty<Type>();
}

public record MayRead<T>() : IMayReadComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = Array.Empty<Type>();
    public static Type[] TypesWrite { get; } = Array.Empty<Type>();
    public static Type[] TypesMayRead { get; } = { typeof(T) };
    public static Type[] TypesMayWrite { get; } = Array.Empty<Type>();
}



public record Write<T>() : IWriteComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = Array.Empty<Type>();
    public static Type[] TypesWrite { get; } = { typeof(T) };
    public static Type[] TypesMayRead { get; } = Array.Empty<Type>();
    public static Type[] TypesMayWrite { get; } = Array.Empty<Type>();
}

public record MayWrite<T>() : IMayWriteComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = Array.Empty<Type>();
    public static Type[] TypesWrite { get; } = Array.Empty<Type>();
    public static Type[] TypesMayRead { get; } = Array.Empty<Type>();
    public static Type[] TypesMayWrite { get; } = { typeof(T) };
}


public struct Something
{
    public static void Hello()
    {
        var q = new FilteredQuery<Read<int>, Write<float>, Filter<Without<long>>>();
    }
}