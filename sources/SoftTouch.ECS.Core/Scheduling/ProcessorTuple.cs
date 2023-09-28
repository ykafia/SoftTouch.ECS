using System.Collections;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public interface IProcessorTuple : IEnumerable<Processor>
{
    public void After();
}

public class ProcessorTuple<T1> : Tuple<T1>
{
    public ProcessorTuple(T1 item1) : base(item1)
    {
    }
    public static implicit operator ProcessorTuple<T1>(T1 processor) => new(processor);
}


public class ProcessorsTuple<T1, T2> : Tuple<T1, T2>, IProcessorTuple
    where T1 : IProcessorTuple
    where T2 : IProcessorTuple
{
    public ProcessorsTuple(T1 item1, T2 item2) : base(item1, item2)
    {
    }

    public void After()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Processor> GetEnumerator()
    {
        
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public static implicit operator ProcessorsTuple<T1,T2>((T1,T2) tuple) => new(tuple.Item1, tuple.Item2);
}

public static class Test
{
    public static void Something()
    {
        ProcessorsTuple<int,int> d = (1,2);
    }
}