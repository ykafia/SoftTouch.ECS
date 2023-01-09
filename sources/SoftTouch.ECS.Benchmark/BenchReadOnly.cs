using SoftTouch.ECS.Components;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS;
using BenchmarkDotNet.Attributes;
using System;
using System.Linq;

namespace SoftTouch.ECS.Benchmark;

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
        for(int i = 0; i < 100; i++)
            w.Update();
    }

    [Benchmark]
    public void BenchArchetype()
    {
        for(int i = 0; i < 100; i++)
            wq.Update();
    }

    [Benchmark]
    public void BenchEnumerableRo()
    {
        for(int i = 0; i < 100; i++)
            wro.Update();
    }

    [Benchmark]
    public void BenchArchetypeRo()
    {
        for(int i = 0; i < 100; i++)
            wqro.Update();
    }
    [Benchmark]
    public void BenchArchetypeSpan()
    {
        for(int i = 0; i < 100; i++)
            wqs.Update();
    }

    [Benchmark]
    public void BenchArchetypeRoSpan()
    {
        for(int i = 0; i < 100; i++)
            wqros.Update();
    }

}