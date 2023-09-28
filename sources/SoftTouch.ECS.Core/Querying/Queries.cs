using CommunityToolkit.HighPerformance.Buffers;
using System.Collections.Immutable;
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

    public ImmutableSortedSet<Type> ImplRead { get; }
    public ImmutableSortedSet<Type> ImplWrite { get; }

    public bool CanRead<T>() where T : struct, IEquatable<T>;
    public bool CanWrite<T>() where T : struct, IEquatable<T>;
    public bool CanRead(Type t);
    public bool CanWrite(Type t);
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


    public ImmutableSortedSet<Type> ImplRead => Read == null ?  ImmutableSortedSet<Type>.Empty : Read.ImplRead;
    public ImmutableSortedSet<Type> ImplWrite => Write == null ?  ImmutableSortedSet<Type>.Empty : Write.ImplWrite;

    public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));
    public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null && ImplWrite.Contains(t));
    public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));
    public bool CanWrite(Type t) => ImplWrite.Contains(t);


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

    public ImmutableSortedSet<Type> ImplRead => Read == null ? ImmutableSortedSet<Type>.Empty : Read.ImplRead;
    public ImmutableSortedSet<Type> ImplWrite => Write == null ? ImmutableSortedSet<Type>.Empty : Write.ImplWrite;

    public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));
    public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null && ImplWrite.Contains(t));
    public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));
    public bool CanWrite(Type t) => ImplWrite.Contains(t);

    public WorldFilteredQueryEnumerator<FilteredQuery<TComp, TFilter>> GetEnumerator() => new(this);
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


    public ImmutableSortedSet<Type> ImplRead => Read == null ? ImmutableSortedSet<Type>.Empty : Read.ImplRead;
    public ImmutableSortedSet<Type> ImplWrite => Write == null ? ImmutableSortedSet<Type>.Empty : Write.ImplWrite;

    public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));
    public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null && ImplWrite.Contains(t));
    public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));
    public bool CanWrite(Type t) => ImplWrite.Contains(t);

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

    public ImmutableSortedSet<Type> ImplRead => Read == null ? ImmutableSortedSet<Type>.Empty : Read.ImplRead;
    public ImmutableSortedSet<Type> ImplWrite => Write == null ? ImmutableSortedSet<Type>.Empty : Write.ImplWrite;

    
    public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));
    public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null &&ImplWrite.Contains(t));
    public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));
    public bool CanWrite(Type t) => ImplWrite.Contains(t);

    public WorldFilteredQueryEnumerator<FilteredQuery<TRead, TWrite, TFilter>> GetEnumerator() => new(this);

}


