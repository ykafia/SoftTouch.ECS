using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Shared.Components;
using System.Linq;
namespace SoftTouch.ECS.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class CreationBench
    {

        public World W1 = new();
        public World W2 = new();
        public World W3 = new();

        [Benchmark]
        public void CreateNewEntitiesSameArchetype()
        {
            W1.Commands.Spawn(new NameComponent{Name = "Lola"});
        }

        [Benchmark]
        public void CreateNewEntitiesSameArchetype2()
        {
            W2.Commands.Spawn(new NameComponent("Lola"),new HealthComponent());
        }

        [Benchmark]
        public void RemoveComponent()
        {
            W2.Commands.Spawn(new NameComponent("Lola"), new HealthComponent());
            //W2[0].Remove<NameComponent>();
        }
        
    }
}
