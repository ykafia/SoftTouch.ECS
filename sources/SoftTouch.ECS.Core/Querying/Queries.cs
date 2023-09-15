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
    public abstract static IMayReadComponent MayRead { get; }
    public abstract static IWriteComponent Write { get; }
    public abstract static IMayWriteComponent MayWrite { get; }

    public Type[] ImplRead { get; }
    public Type[] ImplWrite { get; }
    public Type[] ImplMayRead { get; }
    public Type[] ImplMayWrite { get; }
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
        else if (comp is IMayReadComponent mayread)
            MayRead = mayread;
        else if (comp is IWriteComponent write)
            Write = write;
        else if (comp is IMayWriteComponent mayWrite)
            MayWrite = mayWrite;
    }

    public static IReadComponent Read { get; }
    public static IMayReadComponent MayRead { get; }
    public static IWriteComponent Write { get; }
    public static IMayWriteComponent MayWrite { get; }

    public World World { get; set; }


    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;
    public Type[] ImplMayRead => MayRead.ImplMayRead;
    public Type[] ImplMayWrite => MayWrite.ImplMayWrite;

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
        else if (comp is IMayReadComponent mayread)
            MayRead = mayread;
        else if (comp is IWriteComponent write)
            Write = write;
        else if (comp is IMayWriteComponent mayWrite)
            MayWrite = mayWrite;
        Filters = new TFilter();
    }

    public static IReadComponent Read { get; }
    public static IMayReadComponent MayRead { get; }
    public static IWriteComponent Write { get; }
    public static IMayWriteComponent MayWrite { get; }
    public static IFilterQuery Filters { get; }

    public World World { get; set; }

    public Type[] ImplRead => Read.ImplRead;
    public Type[] ImplWrite => Write.ImplWrite;
    public Type[] ImplMayRead => MayRead.ImplMayRead;
    public Type[] ImplMayWrite => MayWrite.ImplMayWrite;
}

