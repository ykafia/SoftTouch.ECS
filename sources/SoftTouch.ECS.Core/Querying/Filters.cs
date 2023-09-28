using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface IFilterQuery
{
    public abstract static ImmutableSortedSet<Type> WithTypes { get; }
    public abstract static ImmutableSortedSet<Type> WithoutTypes { get; }

    public ImmutableSortedSet<Type> ImplWithTypes { get; }
    public ImmutableSortedSet<Type> ImplWithoutTypes { get; }
}
public interface IFilter
{
    public abstract static ImmutableSortedSet<Type> WithTypes { get; }
    public abstract static ImmutableSortedSet<Type> WithoutTypes { get; }
}

public interface IHasFilter : IFilter { }
public interface IWithoutFilter : IFilter { }

public record Has<T>() : IFilter
    where T : struct, IEquatable<T>
{
    public static ImmutableSortedSet<Type> WithTypes { get; } = ImmutableSortedSet.Create(typeof(T));
    public static ImmutableSortedSet<Type> WithoutTypes => ImmutableSortedSet<Type>.Empty;
}

public record Without<T>() : IFilter
    where T : IEquatable<T>
{
    public static ImmutableSortedSet<Type> WithTypes => ImmutableSortedSet<Type>.Empty;
    public static ImmutableSortedSet<Type> WithoutTypes { get; } = ImmutableSortedSet.Create(typeof(T));
}

public record Filter<T>() : IFilterQuery
    where T : IFilter
{
    public static ImmutableSortedSet<Type> WithTypes { get; } = T.WithTypes; 
    public static ImmutableSortedSet<Type> WithoutTypes { get; } = T.WithoutTypes;

    public ImmutableSortedSet<Type> ImplWithTypes => WithTypes;
    public ImmutableSortedSet<Type> ImplWithoutTypes => WithoutTypes;
}

public record Filter<THas, TWithout>() : IFilterQuery
    where THas : IFilter
    where TWithout : IFilter
{
    public static ImmutableSortedSet<Type> WithTypes { get; } = THas.WithTypes;
    public static ImmutableSortedSet<Type> WithoutTypes { get; } = TWithout.WithoutTypes;

    public ImmutableSortedSet<Type> ImplWithTypes => WithTypes;
    public ImmutableSortedSet<Type> ImplWithoutTypes => WithoutTypes;
}



