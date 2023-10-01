using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Scheduling;

public interface IProcessorSet
{
    public string Name { get; set; }
    public bool IsSingleProcessor { get; }
}


public class SingleProcessor<T1> : IProcessorSet
    where T1 : Processor
{
    public bool IsSingleProcessor => true;

    public T1 Processor { get; }

    public string Name { get; set; }

    public SingleProcessor(T1 processor)
    {
        Name = "single";
        Processor = processor;
    }

    public static implicit operator SingleProcessor<T1>(T1 processor) => new(processor);
}

public class ProcessorSet<T1, T2> : Tuple<T1, T2>, IProcessorSet
    where T1 : IProcessorSet
    where T2 : IProcessorSet

{
    public bool IsSingleProcessor => false;

    public string Name { get; set; }

    public ProcessorSet(string name, T1 item1, T2 item2) : base(item1, item2)
    {
        Name = name;
    }

}

public class ProcessorSet<T1, T2, T3> : Tuple<T1, T2, T3>, IProcessorSet
    where T1 : IProcessorSet
    where T2 : IProcessorSet
    where T3 : IProcessorSet
{
    public string Name { get; set; }
    public bool IsSingleProcessor => false;

    public ProcessorSet(string name, T1 item1, T2 item2, T3 item3) : base(item1, item2, item3)
    {
        Name = name;
    }
}


public class MyProcessor : Processor<Query<Read<int>>>
{
    public MyProcessor(World world) : base(world)
    {
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}

public static partial class ProcessorSetExtensions
{
    public static IProcessorSet ToSet(this (Processor, Processor) processors, string name)
    {
        return new ProcessorSet<SingleProcessor<Processor>, SingleProcessor<Processor>>(name, processors.Item1, processors.Item2);
    }

    public static IProcessorSet ToSet<T>(this (T, Processor) processors, string name)
        where T : IProcessorSet
    {
        return new ProcessorSet<T, SingleProcessor<Processor>>(name, processors.Item1, processors.Item2);
    }
    public static IProcessorSet ToSet<T>(this (Processor, T) processors, string name)
        where T : IProcessorSet
    {
        return new ProcessorSet<SingleProcessor<Processor>, T>(name, processors.Item1, processors.Item2);
    }
    public static IProcessorSet ToSet<T1, T2>(this (T1, T2) processors, string name)
        where T1 : IProcessorSet
        where T2 : IProcessorSet
    {
        return new ProcessorSet<T1, T2>(name, processors.Item1, processors.Item2);
    }

    file static void Test()
    {
        var p = new MyProcessor(new());
        var sets = 
        (
            p, 
            (p,p).ToSet("Something")
        )
        .ToSet("Name");
    }
}