using System.Threading.Tasks.Dataflow;

namespace SoftTouch.ECS.Events;

public class EventKeyBase<T> : IDisposable
{
    // internal readonly Logger Logger;
    internal readonly ulong EventId = EventKeyCounter.New();
    internal readonly string EventName;

    private readonly string broadcastDebug;

    private readonly BroadcastBlock<T> broadcastBlock;

    internal EventKeyBase(string category = "General", string eventName = "Event")
    {
        broadcastBlock = new BroadcastBlock<T>(null, new DataflowBlockOptions { TaskScheduler = EventTaskScheduler.Scheduler });

        EventName = eventName;
        // Logger = GlobalLogger.GetLogger($"Event - {category}");
        broadcastDebug = $"Broadcasting '{eventName}' ({EventId})";
    }

    ~EventKeyBase()
    {
        Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    internal IDisposable Connect(EventReceiverBase<T> target)
    {
        return broadcastBlock.LinkTo(target.BufferBlock!);
    }

    protected void InternalBroadcast(T data)
    {
        // Logger.Debug(broadcastDebug);
        broadcastBlock.Post(data);
    }
}        
