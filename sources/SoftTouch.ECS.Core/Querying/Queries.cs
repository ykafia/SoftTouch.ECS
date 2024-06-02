using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS.Querying;

public interface IWorldQuery
{
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }

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

public delegate void EntityUpdateFunc<T>(ref T component1) where T : struct;
public delegate void EntityUpdateFuncIndexed<T>(EntityMeta index, ref T component1) where T : struct;

public record struct Query<TComp> : IEntityQuery
    where TComp : struct
{

    public static Type[] Types { get; } = [typeof(TComp)];
    public readonly Type[] ImplTypes => Types;
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }

    public readonly bool HasAccessTo<T>() => typeof(T) == typeof(TComp);

    public readonly void ForEach(EntityUpdateFunc<TComp> updater)
    {
        foreach (var e in this)
            updater.Invoke(ref e.Get<TComp>());
    }
    public readonly void ForEachIndexed(EntityUpdateFuncIndexed<TComp> updater)
    {
        foreach (var e in this)
            updater.Invoke(e.EntityIndex, ref e.Get<TComp>());
    }

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
    public Processor CallingProcessor { get; init; }

    public readonly void ForEach(EntityUpdateFunc<TComp> updater)
    {
        foreach (var e in this)
            updater.Invoke(ref e.Get<TComp>());
    }
    public readonly void ForEachIndexed(EntityUpdateFuncIndexed<TComp> updater)
    {
        foreach (var e in this)
            updater.Invoke(e.EntityIndex, ref e.Get<TComp>());
    }

    public readonly WorldFilteredQueryEnumerator<FilteredQuery<TComp, TFilter>> GetEnumerator() => new(this);

    public readonly bool HasAccessTo<T>()
        => typeof(T) == typeof(TComp);
}
