using System.Collections;

namespace SoftTouch.ECS.Events;



public class EventsResource
{
    public Dictionary<Type, EventList> EventStorage { get; } = [];
}