using CommunityToolkit.HighPerformance.Buffers;
using System.ComponentModel;


namespace SoftTouch.ECS.Querying;

public interface IWorldQuery
{
    public World World { get; set; }
}

public interface IEntityQuery : IWorldQuery
{
    public abstract static IReadComponent Read { get; }
    public abstract static IWriteComponent Write { get; }

    public Type[] ImplRead { get; }
    public Type[] ImplWrite { get; }
}

public interface IFilteredEntityQuery : IEntityQuery
{
    public abstract static IFilterQuery Filters { get; }
}

public record struct Query<TComp> : IEntityQuery
    where TComp : IComponentQuery, new()
{

    static Query()
    {
        var comp = new TComp();
        if (comp is IReadComponent read)
            Read = read;
        else if (comp is IWriteComponent write)
            Write = write;
    }

    public static IReadComponent Read { get; }
    public static IWriteComponent Write { get; }

    public World World { get; set; }


    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;

    public WorldQueryEnumerator<Query<TComp>> GetEnumerator() => new(this);
}
public record struct FilteredQuery<TComp, TFilter> : IFilteredEntityQuery
    where TComp : IComponentQuery, new()
    where TFilter : IFilterQuery, new()
{
    static FilteredQuery()
    {
        var comp = new TComp();
        if (comp is IReadComponent read)
            Read = read;
        else if (comp is IWriteComponent write)
            Write = write;
        Filters = new TFilter();
    }

    public static IReadComponent Read { get; }
    public static IWriteComponent Write { get; }
    public static IFilterQuery Filters { get; }

    public World World { get; set; }

    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;
}


public record struct Query<TRead, TWrite> : IEntityQuery
    where TRead : IReadComponent, new()
    where TWrite : IWriteComponent, new()
{

    static Query()
    {
        Read = new TRead();
        Write = new TWrite();
    }

    public static IReadComponent Read { get; }
    public static IWriteComponent Write { get; }

    public World World { get; set; }


    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;

    public WorldQueryEnumerator<Query<TRead,TWrite>> GetEnumerator() => new(this);
}
public record struct FilteredQuery<TRead, TWrite, TFilter> : IFilteredEntityQuery
    where TRead : IReadComponent, new()
    where TWrite : IWriteComponent, new()
    where TFilter : IFilterQuery, new()
{
    static FilteredQuery()
    {
        Read = new TRead();
        Write = new TWrite();
        Filters = new TFilter();
    }

    public static IReadComponent Read { get; }
    public static IWriteComponent Write { get; }
    public static IFilterQuery Filters { get; }

    public World World { get; set; }

    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;

    public WorldFilteredQueryEnumerator<FilteredQuery<TRead, TWrite, TFilter>> GetEnumerator() => new(this);

}


