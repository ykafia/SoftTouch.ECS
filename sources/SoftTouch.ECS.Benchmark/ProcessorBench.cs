using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS.Components;
using System.Linq;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Benchmark
{
    [MemoryDiagnoser]
    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
    public class ProcessorBench
    {

        public World W1 = new();
        public World W2 = new();
        public World W3 = new();

        static int Size = 10000;

        public ProcessorBench()
        {
            for (int i = 0; i < Size; i++)
            {
                W1.CreateEntity()
                    .With<HealthComponent>();
                W1.Add<Processor1>();
                W2.CreateEntity()
                    .With<NameComponent>()
                    .With<HealthComponent>();
                W2.Add<Processor2>();
                W3.CreateEntity()
                    .With<NameComponent>()
                    .With<HealthComponent>()
                    .With<ModelComponent>();
                W3.Add<Processor3>();
                
            }
        }

        [Benchmark]
        public void Processor1()
        {
            W1.Update();
        }

        [Benchmark]
        public void Processor2()
        {
            W2.Update();
        }

        [Benchmark]
        public void Processor3()
        {
            W2.Update();
        }
        
    }
}
