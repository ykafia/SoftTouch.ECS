namespace ECSharp;

public static class ProcessorExtensions
{
    public static Processor After(this Processor p, Processor other)
    {
        p.World.Processors.MoveAfter(other, p);
        return p;
    }
}