using CommunityToolkit.HighPerformance.Buffers;
using System.Collections.Immutable;
using System.ComponentModel;


namespace SoftTouch.ECS.Querying;

public interface IWorldQuery
{
    internal World World { get; set; }
}

public interface IEntityQuery : IWorldQuery
{
    public abstract static Type[] Types { get; }
    public Type[] ImplTypes { get; }
    public bool HasAccessTo<T>();
}

public interface IFilteredEntityQuery : IEntityQuery
{
    public abstract static IFilterQuery Filters { get; }
}

public record struct Query<TComp> : IEntityQuery
    where TComp : struct
{

    public static Type[] Types { get; } = [typeof(TComp)];
    public readonly Type[] ImplTypes => Types;
    public World World { get; set; }

    public readonly bool HasAccessTo<T>() => typeof(T) == typeof(TComp);


    public readonly WorldQueryEnumerator<Query<TComp>> GetEnumerator() => new(this);
}
public record struct FilteredQuery<TComp, TFilter> : IFilteredEntityQuery
    where TComp : struct
    where TFilter : IFilterQuery, new()
{
    public static Type[] Types { get; } = [typeof(TComp)];
    public readonly Type[] ImplTypes => Types;
    public static IFilterQuery Filters { get; } = new TFilter();

    public World World { get; set; }

    public WorldFilteredQueryEnumerator<FilteredQuery<TComp, TFilter>> GetEnumerator() => new(this);

    public readonly bool HasAccessTo<T>()
        => typeof(T) == typeof(TComp);
}
