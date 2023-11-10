using CommunityToolkit.HighPerformance.Buffers;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;


namespace SoftTouch.ECS.Querying;



public interface IComponentQuery 
{
    public static abstract TypeSet TypesRead { get; }
    public static abstract TypeSet TypesWrite { get; }

    public TypeSet ImplRead { get; }
    public TypeSet ImplWrite { get; }
}
public interface IReadComponent : IComponentQuery { }
public interface IWriteComponent : IComponentQuery { }


public record Read<T>() : IReadComponent
    where T : struct
{
    public static TypeSet TypesRead { get; } = TypeSet.Create(typeof(T));
    public static TypeSet TypesWrite { get; } = TypeSet.Empty;

    public TypeSet ImplRead => TypesRead;
    public TypeSet ImplWrite => TypesWrite;
}

public record Write<T>() : IWriteComponent
    where T : struct
{
    public static TypeSet TypesRead { get; } = TypeSet.Empty;
    public static TypeSet TypesWrite { get; } = TypeSet.Create(typeof(T));

    public TypeSet ImplRead => TypesRead;
    public TypeSet ImplWrite => TypesWrite;
}
