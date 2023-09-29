using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public class ProcessorSet<T1, T2> : Tuple<T1, T2>
    where T1 : Processor
    where T2 : Processor

{
    public ProcessorSet(T1 item1, T2 item2) : base(item1, item2)
    {
    }

    public static implicit operator ProcessorSet<T1,T2>((T1,T2) tuple) => new(tuple.Item1,tuple.Item2);
}

public class ProcessorSet<T1, T3> : Tuple<T1, T2>
    where T1 : Processor
    where T2 : Processor
    where T3 : Processor

{
    public ProcessorSet(T1 item1, T2 item2) : base(item1, item2)
    {
    }

    public static implicit operator ProcessorSet<T1, T2>((T1, T2) tuple) => new(tuple.Item1, tuple.Item2);
}