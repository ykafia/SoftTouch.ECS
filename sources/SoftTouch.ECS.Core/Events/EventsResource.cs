using System.Collections;
using CommunityToolkit.HighPerformance;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;

public class EventsResource
{
    public Dictionary<Type, EventList> EventStorage { get; } = [];

    public void Add<T>(T data)
        where T : struct, IEvent
    {
        if (EventStorage.TryGetValue(typeof(T), out var list))
            ((EventList<T>)list).Add(data);
        else
        {
            EventList<T> l =[data];
            EventStorage.Add(typeof(T), l);
        }
    }
    public ProcessorReader<T> Receive<T>(Processor processor)
        where T : struct, IEvent
    {
        if (EventStorage.TryGetValue(typeof(T), out var list))
            return ((EventList<T>)list).GetOrSubscribe(processor);
        else
            return new(null, processor);
    }

    public void Update()
    {
        foreach (var l in EventStorage.Values)
            l.UpdateEvents();
    }
}