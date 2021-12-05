using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using ECSharp.Components;
using System.Linq;
namespace ECSharp.Benchmark
{
    public class Benchmarks
    {

        public World W1 = new World();
        public World W2 = new World();
        public World W3 = new World();

        [Benchmark]
        public void CreateNewEntitiesSameArchetype()
        {
            for(int i = 0; i < 10000; i++)
                W1.CreateEntity().With(new NameComponent{Name = "Lola"}).Build();
        }

        [Benchmark]
        public void CreateNewEntitiesSameArchetype2()
        {
            for(int i = 0; i < 10000; i++)
                W2.CreateEntity().With(new NameComponent{Name = "Lola"}).With(new HealthComponent()).Build();
        }

        [Benchmark]
        public void RemoveComponent()
        {
            for(int i = 0; i < 5000; i++)
                W3.CreateEntity().With(new NameComponent{Name = "Lola"}).With(new HealthComponent()).Build();
            for(int i = 0; i < 5000; i++)
                W3[i].Remove<NameComponent>();
        }
        
    }
}
