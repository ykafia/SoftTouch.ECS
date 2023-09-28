using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Shared.Components;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Collections.Immutable;
using System.Linq;

namespace SoftTouch.ECS.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class SuperSetBench
    {
        public Type[] types = { typeof(int), typeof(double), typeof(float), typeof(uint), typeof(bool)};
        public ImmutableHashSet<Type> typesToCheck = ImmutableHashSet.Create(typeof(int), typeof(uint), typeof(float));
        public ImmutableSortedSet<Type> typesToCheck2 = ImmutableSortedSet.Create(typeof(int), typeof(uint), typeof(float));
        public SuperSetBench()
        {
            
        }
        [Benchmark]
        public void SuperSet()
        {
            var b = typesToCheck.IsSubsetOf(types);
        }

        [Benchmark]
        public void SuperSortedSet()
        {
            var b = typesToCheck2.IsSubsetOf(types);
        }
        [Benchmark]
        public void SuperSortedSet2()
        {
            bool b = true;
            foreach (var t in typesToCheck2)
                if (!types.Contains(t))
                    b = false;
        }


    }
}
