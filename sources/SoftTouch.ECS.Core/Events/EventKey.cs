namespace SoftTouch.ECS.Events;

/// <summary>
/// Used mostly for debug, to identify events
/// </summary>
internal static class EventKeyCounter
{
    private static long eventKeysCounter;

    public static ulong New()
    {
        return (ulong)Interlocked.Increment(ref eventKeysCounter);
    }
}

public sealed class EventKey<T> : EventKeyBase<T>
{
    public EventKey(string category = "General", string eventName = "Event") : base(category, eventName)
    {
    }

    /// <summary>
    /// Broadcasts the event to all the receivers
    /// </summary>
    public void Broadcast(T data)
    {
        InternalBroadcast(data);
    }
}

/// <summary>
/// Creates a new EventKey used to broadcast events.
/// </summary>
public sealed class EventKey : EventKeyBase<bool>
{
    public EventKey(string category = "General", string eventName = "Event") : base(category, eventName)
    {
    }

    /// <summary>
    /// Broadcasts the event to all the receivers
    /// </summary>
    public void Broadcast()
    {
        InternalBroadcast(true);
    }
}