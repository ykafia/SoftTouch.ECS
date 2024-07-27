using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface IFilterQuery
{
    public abstract static TypeSet WithTypes { get; }
    public abstract static TypeSet WithoutTypes { get; }

    public TypeSet ImplWithTypes { get; }
    public TypeSet ImplWithoutTypes { get; }
}
public interface IFilter
{
    public abstract static TypeSet WithTypes { get; }
    public abstract static TypeSet WithoutTypes { get; }
}

public interface IHasFilter : IFilter { }
public interface IWithoutFilter : IFilter { }

public record With<T>() : IFilter
    where T : struct, IEquatable<T>
{
    public static TypeSet WithTypes { get; } = TypeSet.Create(typeof(T));
    public static TypeSet WithoutTypes => TypeSet.Empty;
}

public record Without<T>() : IFilter
    where T : IEquatable<T>
{
    public static TypeSet WithTypes => TypeSet.Empty;
    public static TypeSet WithoutTypes { get; } = TypeSet.Create(typeof(T));
}

public record Filter<T>() : IFilterQuery
    where T : IFilter
{
    public static TypeSet WithTypes { get; } = T.WithTypes; 
    public static TypeSet WithoutTypes { get; } = T.WithoutTypes;

    public TypeSet ImplWithTypes => WithTypes;
    public TypeSet ImplWithoutTypes => WithoutTypes;
}

public record Filter<THas, TWithout>() : IFilterQuery
    where THas : IFilter
    where TWithout : IFilter
{
    public static TypeSet WithTypes { get; } = THas.WithTypes;
    public static TypeSet WithoutTypes { get; } = TWithout.WithoutTypes;

    public TypeSet ImplWithTypes => WithTypes;
    public TypeSet ImplWithoutTypes => WithoutTypes;
}





