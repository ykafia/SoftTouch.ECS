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
    public readonly Type EventDataType => typeof(T);
    public readonly IReadOnlyList<T> Read() => ((EventList<T>)World.Resources.Get<EventsResource>().EventStorage[typeof(T)]).AsReadOnly();
}

public struct EventWriter<T> : IEventWriter
    where T : struct, IEvent
{
    public World World { get; set; }
    public readonly Type EventDataType => typeof(T);
    public readonly IReadOnlyList<T> Read() => ((EventList<T>)World.Resources.Get<EventsResource>().EventStorage[typeof(T)]).AsReadOnly();
}

