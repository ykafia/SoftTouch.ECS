using CommunityToolkit.HighPerformance.Buffers;
using System.Runtime.CompilerServices;


namespace SoftTouch.ECS.Querying;



public interface IComponentQuery 
{
    public static abstract Type[] TypesRead { get; }
    public static abstract Type[] TypesWrite { get; }

    public Type[] ImplRead { get; }
    public Type[] ImplWrite { get; }
}
public interface IReadComponent : IComponentQuery { }
public interface IWriteComponent : IComponentQuery { }


public record Read<T>() : IReadComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = { typeof(T) };
    public static Type[] TypesWrite { get; } = Array.Empty<Type>();

    public Type[] ImplRead => TypesRead;
    public Type[] ImplWrite => TypesWrite;
}

public record Write<T>() : IWriteComponent
    where T : struct
{
    public static Type[] TypesRead { get; } = Array.Empty<Type>();
    public static Type[] TypesWrite { get; } = { typeof(T) };

    public Type[] ImplRead => TypesRead;
    public Type[] ImplWrite => TypesWrite;
}
