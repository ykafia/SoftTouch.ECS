using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using ECSharp.Components;
using System.Linq;
using ECSharp.Processors;
using ECSharp.Arrays;

namespace ECSharp.Benchmark;


[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class CopyBench
{

    public ComponentList<HealthComponent> comps1 = new(Size);
    public ComponentList<HealthComponent> comps2 = new(Size);


    static int Size = 10;

    public CopyBench()
    {
        for (int i = 0; i < Size; i++)
        {
            comps1.Add(new(i,i));
        }
    }
    [Benchmark]
    public void CopyToList()
    {
        comps2 = (ComponentList<HealthComponent>)comps1.ToList();
    }
    [Benchmark]
    public void CopyRange()
    {
        comps2.AddRange(comps1);
    }
    [Benchmark]
    public void CopySpan()
    {
        comps1.AsSpan().CopyTo(comps2.AsSpan());
    }

}