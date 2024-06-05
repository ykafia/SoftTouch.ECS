using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;


public interface IEvent
{
    public int FrameAge { get; set; }
}