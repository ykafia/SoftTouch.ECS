using System.Collections;
using CommunityToolkit.HighPerformance;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;

public struct ProcessorReader(EventsResource resource, Processor processor, Type eventType)
{
    public EventsResource Resource { get; init; } = resource;
    public Processor Processor { get; init; } = processor;
    public Type EventType { get; init; } = eventType;
    public int ReadCount { get; set; } = 0;
}

public ref struct EventEnumerator<T>(EventsResource resource, ref ProcessorReader reader)
    where T : struct, IEvent
{
    readonly EventsResource resource = resource;
    ref ProcessorReader processorRead = ref reader;
    readonly EventList<T> Events => (EventList<T>)resource.EventStorage[typeof(T)];
    public readonly T Current => Events[processorRead.ReadCount];
    public readonly bool MoveNext()
    {
        processorRead.ReadCount += 1;
        if (processorRead.ReadCount < Events.Count - 1)
            return true;
        return false;
    }
}


public class EventsResource
{
    public Dictionary<Type, EventList> EventStorage { get; } = [];
    public List<ProcessorReader> ProcessorReads { get; } = [];
}