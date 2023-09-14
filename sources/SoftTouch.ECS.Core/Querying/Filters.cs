using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public interface IFilterQuery
{
    public abstract static Type[] WithTypes { get; }
    public abstract static Type[] WithoutTypes { get; }
}
public interface IFilter
{
    public abstract static Type[] WithTypes { get; }
    public abstract static Type[] WithoutTypes { get; }
}

public interface IHasFilter : IFilter { }
public interface IWithoutFilter : IFilter { }

public record Has<T>() : IFilter
    where T : struct, IEquatable<T>
{
    public static Type[] WithTypes { get; } = { typeof(T) };
    public static Type[] WithoutTypes => Array.Empty<Type>();
}

public record Without<T>() : IFilter
    where T : IEquatable<T>
{
    public static Type[] WithTypes => Array.Empty<Type>();
    public static Type[] WithoutTypes { get; } = { typeof(T) };
}

public record Filter<T>() : IFilterQuery
    where T : IFilter
{
    public static Type[] WithTypes { get; } = T.WithTypes; 
    public static Type[] WithoutTypes { get; } = T.WithoutTypes;
}

public record Filter<THas, TWithout>() : IFilterQuery
    where THas : IFilter
    where TWithout : IFilter
{
    public static Type[] WithTypes { get; } = THas.WithTypes;
    public static Type[] WithoutTypes { get; } = TWithout.WithoutTypes;
}



