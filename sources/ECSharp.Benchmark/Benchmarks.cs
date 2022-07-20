using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using ECSharp.Components;
using System.Linq;
namespace ECSharp.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class Benchmarks
    {

        public World W1 = new World();
        public World W2 = new World();
        public World W3 = new World();

        [Benchmark]
        public void CreateNewEntitiesSameArchetype()
        {
            W1.CreateEntity().With(new NameComponent{Name = "Lola"}).Build();
        }

        [Benchmark]
        public void CreateNewEntitiesSameArchetype2()
        {
            W2.CreateEntity().With(new NameComponent{Name = "Lola"}).With(new HealthComponent()).Build();
        }

        [Benchmark]
        public void RemoveComponent()
        {
            W2.CreateEntity().With(new NameComponent{Name = "Lola"}).With(new HealthComponent()).Build();
            W2[0].Remove<NameComponent>();
        }
        
    }
}
