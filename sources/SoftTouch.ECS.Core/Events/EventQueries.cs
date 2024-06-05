using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Events;


public interface IEventQuery : IWorldQuery
{
    public Type EventDataType { get; }
}

public interface IEventReader : IEventQuery;
public interface IEventWriter : IEventQuery;

public struct EventReader<T> : IEventReader
    where T : struct, IEvent
{
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public readonly Type EventDataType => typeof(T);
    public readonly ProcessorReader<T> Receive() => World.GetResource<EventsResource>().Receive<T>(CallingProcessor);
}

public struct EventWriter<T> : IEventWriter
    where T : struct, IEvent
{
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public readonly Type EventDataType => typeof(T);
    public readonly ProcessorReader<T> Receive() => World.GetResource<EventsResource>().Receive<T>(CallingProcessor);
    public readonly void Broadcast(T item) => World.GetResource<EventsResource>().Add(item);
}

