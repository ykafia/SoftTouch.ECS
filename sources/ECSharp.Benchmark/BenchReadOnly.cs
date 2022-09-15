using ECSharp.Components;
using ECSharp.Processors;
using ECSharp;
using BenchmarkDotNet.Attributes;

namespace ECSharp.Benchmark;

[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class BenchReadOnly
{
    World w;
    World wro;
    World wq;
    World wqro;
    World wqs;
    World wqros;
    public BenchReadOnly()
    {
        w = new();
        wro = new();
        wq = new();
        wqro = new();
        wqs = new();
        wqros = new();
        for (int i = 0; i < 100; i++)
        {
            w.CreateEntity().With(new HealthComponent(i * 10, i * 11));
            wro.CreateEntity().With(new ROHealthComponent(i * 10, i * 11));
            wq.CreateEntity().With(new HealthComponent(i * 10, i * 11));
            wqro.CreateEntity().With(new ROHealthComponent(i * 10, i * 11));
            wqs.CreateEntity().With(new HealthComponent(i * 10, i * 11));
            wqros.CreateEntity().With(new ROHealthComponent(i * 10, i * 11));
        }
        w.Add<HealthProcessorE>();
        wro.Add<HealthProcessorRO>();
        wq.Add<HealthProcessorQ>();
        wqro.Add<HealthProcessorQRO>();
        wqs.Add<HealthProcessorQ>();
        wqros.Add<HealthProcessorQRO>();

    }

    [Benchmark]
    public void BenchEnumerable()
    {
        w.Run(100);
    }

    [Benchmark]
    public void BenchArchetype()
    {
        wq.Run(100);
    }

    [Benchmark]
    public void BenchEnumerableRo()
    {
        wro.Run(100);
    }

    [Benchmark]
    public void BenchArchetypeRo()
    {
        wqro.Run(100);
    }
    [Benchmark]
    public void BenchArchetypeSpan()
    {
        wqs.Run(100);
    }

    [Benchmark]
    public void BenchArchetypeRoSpan()
    {
        wqros.Run(100);
    }

}