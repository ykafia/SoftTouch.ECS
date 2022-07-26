namespace ECSharp.Events;
internal class EventTaskScheduler : TaskScheduler
{
    public static readonly EventTaskScheduler Scheduler = new EventTaskScheduler();

    protected override void QueueTask(Task task)
    {
        TryExecuteTask(task);
    }

    protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        return false;
    }

    protected override IEnumerable<Task> GetScheduledTasks()
    {
        return null;
    }
}