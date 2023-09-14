namespace SoftTouch.ECS;

public static class ProcessorExtensions
{
    public static IProcessor After(this IProcessor p, IProcessor other)
    {
        p.World.Processors.MoveAfter(other, p);
        return p;
    }
}