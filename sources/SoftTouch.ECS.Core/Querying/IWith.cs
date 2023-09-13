using CommunityToolkit.HighPerformance.Buffers;


namespace SoftTouch.ECS.Core.Queries;


public interface IWith
{
    public MemoryOwner<Type> With { get; }
}
public interface IWithAll
{
    public MemoryOwner<Type> WithAll { get; }
}

public interface IWithout
{
    public MemoryOwner<Type> WithAll { get; }
}
public interface IWithOnly
{
    public MemoryOwner<Type> WithOnly { get; }
}