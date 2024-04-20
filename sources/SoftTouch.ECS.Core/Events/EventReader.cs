using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Events;


public interface IEventQuery : IWorldQuery;

public struct EventReader<T> : IEventQuery
    where T : struct, IEvent
{
    public World World { get; set; }
    public readonly IReadOnlyList<T> Read() => ((EventList<T>)World.Resources.Get<EventsResource>().EventStorage[typeof(T)]).AsReadOnly();
}