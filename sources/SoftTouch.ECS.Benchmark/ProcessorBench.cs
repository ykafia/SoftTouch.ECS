//using System;
//using BenchmarkDotNet;
//using BenchmarkDotNet.Attributes;
//using System.Linq;
//using SoftTouch.ECS.Shared.Processors;
//using SoftTouch.ECS.Shared.Components;

//namespace SoftTouch.ECS.Benchmark
//{
//    [MemoryDiagnoser]
//    [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
//    public class ProcessorBench
//    {

//        public World W1 = new();
//        public World W2 = new();
//        public World W3 = new();

//        static int Size = 10000;

//        public ProcessorBench()
//        {
//            for (int i = 0; i < Size; i++)
//            {
//                W1.Commands.Spawn<HealthComponent>();
//                W1.AddProcessor<Processor1>();
//                W2.Commands.Spawn<NameComponent, HealthComponent>();
//                W2.AddProcessor(new Processor2());
//                W3.Commands.Spawn<NameComponent, HealthComponent, ModelComponent>();
//                W3.AddProcessor<Processor3>();
//            }
//        }

//        [Benchmark]
//        public void Processor1()
//        {
//            W1.Update();
//        }

//        [Benchmark]
//        public void Processor2()
//        {
//            W2.Update();
//        }

//        [Benchmark]
//        public void Processor3()
//        {
//            W2.Update();
//        }
        
//    }
//}
