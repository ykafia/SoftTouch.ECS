using CommunityToolkit.HighPerformance.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;


namespace SoftTouch.ECS.Querying;



public interface IComponentQuery 
{
    public static abstract ImmutableSortedSet<Type> TypesRead { get; }
    public static abstract ImmutableSortedSet<Type> TypesWrite { get; }

    public ImmutableSortedSet<Type> ImplRead { get; }
    public ImmutableSortedSet<Type> ImplWrite { get; }
}
public interface IReadComponent : IComponentQuery { }
public interface IWriteComponent : IComponentQuery { }


public record Read<T>() : IReadComponent
    where T : struct
{
    public static ImmutableSortedSet<Type> TypesRead { get; } = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)),typeof(T));
    public static ImmutableSortedSet<Type> TypesWrite { get; } = ImmutableSortedSet<Type>.Empty;

    public ImmutableSortedSet<Type> ImplRead => TypesRead;
    public ImmutableSortedSet<Type> ImplWrite => TypesWrite;
}

public record Write<T>() : IWriteComponent
    where T : struct
{
    public static ImmutableSortedSet<Type> TypesRead { get; } = ImmutableSortedSet<Type>.Empty;
    public static ImmutableSortedSet<Type> TypesWrite { get; } = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)),typeof(T));

    public ImmutableSortedSet<Type> ImplRead => TypesRead;
    public ImmutableSortedSet<Type> ImplWrite => TypesWrite;
}
