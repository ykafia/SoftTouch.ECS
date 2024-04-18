using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;


public interface IEvent
{
    public long CreationFrame { get; }
    public HashSet<Processor> AlreadyRead { get; }
}

public interface IEvent<T> : IEvent
    where T : struct
{
    public T Data { get; }
}

public struct Event<T>(T data, long creationFrame) : IEvent<T>
    where T : struct
{
    public readonly T Data { get; } = data;

    public readonly long CreationFrame { get; } = creationFrame;

    public readonly HashSet<Processor> AlreadyRead => [];
}