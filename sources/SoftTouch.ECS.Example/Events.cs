using SoftTouch.ECS.Events;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Example;


public struct ChangedAge(long creationFrame) : IEvent
{
    public int PreviousAge { get; set; }
    public int Age { get; set; }
    public long CreationFrame { get; set; } = creationFrame;

    public HashSet<Processor> AlreadyRead { get; } = [];
    public int FrameAge { get; set; }
}

public struct ChangedName(long creationFrame) : IEvent
{
    public string PreviousName { get; set; }
    public string Name { get; set; }
    public long CreationFrame { get; set; } = creationFrame;

    public HashSet<Processor> AlreadyRead { get; } = [];
    public int FrameAge { get; set; }
}


public struct TimeCount(TimeSpan delay)
{
    public TimeSpan Delay { get; set; } = delay;
    public TimeSpan Time { get; set; } = TimeSpan.Zero;
}