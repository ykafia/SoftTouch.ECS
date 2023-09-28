using CommunityToolkit.HighPerformance.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;


namespace SoftTouch.ECS.Querying;



public interface IComponentQuery 
{
    public static abstract ImmutableHashSet<Type> TypesRead { get; }
    public static abstract ImmutableHashSet<Type> TypesWrite { get; }

    public ImmutableHashSet<Type> ImplRead { get; }
    public ImmutableHashSet<Type> ImplWrite { get; }
}
public interface IReadComponent : IComponentQuery { }
public interface IWriteComponent : IComponentQuery { }


public record Read<T>() : IReadComponent
    where T : struct
{
    public static ImmutableHashSet<Type> TypesRead { get; } = ImmutableHashSet.Create(typeof(T));
    public static ImmutableHashSet<Type> TypesWrite { get; } = ImmutableHashSet<Type>.Empty;

    public ImmutableHashSet<Type> ImplRead => TypesRead;
    public ImmutableHashSet<Type> ImplWrite => TypesWrite;
}

public record Write<T>() : IWriteComponent
    where T : struct
{
    public static ImmutableHashSet<Type> TypesRead { get; } = ImmutableHashSet<Type>.Empty;
    public static ImmutableHashSet<Type> TypesWrite { get; } = ImmutableHashSet.Create(typeof(T));

    public ImmutableHashSet<Type> ImplRead => TypesRead;
    public ImmutableHashSet<Type> ImplWrite => TypesWrite;
}
