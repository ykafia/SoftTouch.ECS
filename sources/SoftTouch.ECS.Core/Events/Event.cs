using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;


public interface IEvent;
public interface IEvent<T> : IEvent
    where T : struct
{
    public T Data { get; }
}

public readonly struct Event<T>(T data) : IEvent<T>
    where T : struct
{
    public readonly T Data { get; } = data;
    public static implicit operator Event<T>(T data) => new(data);
}