using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using ECSharp.Components;
using System.Linq;
namespace ECSharp.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class BenchAlloc
    {
        NameComponent?[] nullables;
        NameComponent[] normal;
        
        public BenchAlloc()
        {
            nullables = new NameComponent?[10];
            normal = new NameComponent[10];
        }
        [Benchmark]
        public void AllocComponent()
        {
            for(int i = 0; i< normal.Length; i++)
                normal[i].Name = "Lola" + i;         
        }
        [Benchmark]
        public void AllocNullComponent()
        {
            for(int i = 0; i< nullables.Length; i++)
                nullables[i] = i % 5 == 0 ? null : new NameComponent{Name = "Lola" + i};  
        }
        
    }
}
