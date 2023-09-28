using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface IFilterQuery
{
    public abstract static ImmutableHashSet<Type> WithTypes { get; }
    public abstract static ImmutableHashSet<Type> WithoutTypes { get; }

    public ImmutableHashSet<Type> ImplWithTypes { get; }
    public ImmutableHashSet<Type> ImplWithoutTypes { get; }
}
public interface IFilter
{
    public abstract static ImmutableHashSet<Type> WithTypes { get; }
    public abstract static ImmutableHashSet<Type> WithoutTypes { get; }
}

public interface IHasFilter : IFilter { }
public interface IWithoutFilter : IFilter { }

public record Has<T>() : IFilter
    where T : struct, IEquatable<T>
{
    public static ImmutableHashSet<Type> WithTypes { get; } = ImmutableHashSet.Create(typeof(T));
    public static ImmutableHashSet<Type> WithoutTypes => ImmutableHashSet<Type>.Empty;
}

public record Without<T>() : IFilter
    where T : IEquatable<T>
{
    public static ImmutableHashSet<Type> WithTypes => ImmutableHashSet<Type>.Empty;
    public static ImmutableHashSet<Type> WithoutTypes { get; } = ImmutableHashSet.Create(typeof(T));
}

public record Filter<T>() : IFilterQuery
    where T : IFilter
{
    public static ImmutableHashSet<Type> WithTypes { get; } = T.WithTypes; 
    public static ImmutableHashSet<Type> WithoutTypes { get; } = T.WithoutTypes;

    public ImmutableHashSet<Type> ImplWithTypes => WithTypes;
    public ImmutableHashSet<Type> ImplWithoutTypes => WithoutTypes;
}

public record Filter<THas, TWithout>() : IFilterQuery
    where THas : IFilter
    where TWithout : IFilter
{
    public static ImmutableHashSet<Type> WithTypes { get; } = THas.WithTypes;
    public static ImmutableHashSet<Type> WithoutTypes { get; } = TWithout.WithoutTypes;

    public ImmutableHashSet<Type> ImplWithTypes => WithTypes;
    public ImmutableHashSet<Type> ImplWithoutTypes => WithoutTypes;
}



