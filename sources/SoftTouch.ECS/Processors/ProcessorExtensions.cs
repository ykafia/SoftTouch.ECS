namespace SoftTouch.ECS;

public static class ProcessorExtensions
{
    public static IProcessor After(this IProcessor<World> p, IProcessor<World> other)
    {
        p.World.Processors.MoveAfter(other, p);
        return p;
    }
}