using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Shared.Processors;
using SoftTouch.ECS.Shared.Components;
using System.Linq;
using SoftTouch.ECS.Arrays;
using System.Collections.Generic;

namespace SoftTouch.ECS.Benchmark;


[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class CopyBench
{
    public class Person : ICloneable
    {
        public int Age {get;set;}
        public float Height {get;set;}

        public Person(int a, float h)
        {
            Age = a;
            Height = h;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    public ComponentArray<HealthComponent> comps1 = new(Size);
    public List<Person> compo1 = new(Size);

    public ComponentArray<HealthComponent> comps2 = new(Size);
    public List<Person> compo2 = new(Size);

    static int Size = 10;

    public CopyBench()
    {
        for (int i = 0; i < Size; i++)
        {
            comps1.Add(new(i,i));
            compo1.Add(new(i,i));
        }
    }
    [Benchmark]
    public void CopyRangeObjects()
    {
        compo2.AddRange(compo1.Select(x => x.Clone()).Cast<Person>());
    }
    [Benchmark]
    public void CopyRangeStructs()
    {
        comps1.AddRange(comps1);
    }
}