using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Shared.Processors;
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
            w.Commands.Spawn(new HealthComponent(i * 10, i * 11));
            wro.Commands.Spawn(new ROHealthComponent(i * 10, i * 11));
            wq.Commands.Spawn(new HealthComponent(i * 10, i * 11));
            wqro.Commands.Spawn(new ROHealthComponent(i * 10, i * 11));
            wqs.Commands.Spawn(new HealthComponent(i * 10, i * 11));
            wqros.Commands.Spawn(new ROHealthComponent(i * 10, i * 11));
        }
        //w.AddProcessor<HealthProcessorE>();
        //wro.AddProcessor<HealthProcessorRO>();
        //wq.AddProcessor<HealthProcessorQ>();
        //wqro.AddProcessor<HealthProcessorQRO>();
        //wqs.AddProcessor<HealthProcessorQ>();
        //wqros.AddProcessor<HealthProcessorQRO>();

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