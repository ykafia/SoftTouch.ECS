using System.Collections;
using CommunityToolkit.HighPerformance;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;

public class EventsResource
{
    public Dictionary<Type, EventList> EventStorage { get; } = [];
    public List<ProcessorReader> ProcessorReads { get; } = [];

    public void Reset()
    {
        foreach (var l in EventStorage.Values)
            l.ResetEvents();
    }
}