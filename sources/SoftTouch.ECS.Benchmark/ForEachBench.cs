// using BenchmarkDotNet;
// using BenchmarkDotNet.Attributes;
// using SoftTouch.ECS;
// using SoftTouch.ECS.Shared.Components;
// using SoftTouch.ECS.Shared.Processors;
// using System;

// namespace SoftTouch.ECS.Benchmark;


// [MemoryDiagnoser]
// [SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
// public class ForEachBench
// {
// 	World w1;
// 	World w2;

// 	public ForEachBench()
// 	{
// 		w1 = new();

// 		//w1.Commands.Spawn(new NameComponent("Martha" )).With<HealthComponent>();
// 		//w1.Commands.Spawn(new NameComponent("Martha" ), new HealthComponent());
// 		//w1.Commands.Spawn(new NameComponent("Martha" ), default(int));
// 		//w1.Commands.Spawn(new NameComponent("Martha" ), new HealthComponent(), (1, 5));
// 		//w1.Commands.Spawn(new NameComponent("Martha" ), new HealthComponent());

//   //      static void WithDeconstruct(Query<NameComponent,HealthComponent> q1)
//   //      {
// 		//	foreach(var e in q1)
// 		//	{
// 		//		var (name, health) = e;
// 		//		e.Set<NameComponent>(new ("John Wayne"));
// 		//	}
// 		//}

//   //      w1.AddProcessor((Query<NameComponent,HealthComponent> q) => WithDeconstruct(q));
// 		//w1.Start();

// 		//w2 = new();

// 		//w2.Commands.Spawn(
// 		//new NameComponent("Martha"));
// 		//w2.Commands.Spawn(
// 		//new NameComponent("Martha"),
// 		//new HealthComponent());
// 		//w2.Commands.Spawn(
// 		//new NameComponent("Martha"), default(int));
// 		//w2.Commands.Spawn(
// 		//new NameComponent("Martha"),
// 		//new HealthComponent(),
// 		//(1, 5));
// 		//w2.Commands.Spawn(
// 		//new NameComponent("Martha"),
// 		//new HealthComponent());

//   //      static void SimpleForeach(Query<NameComponent> q1)
//   //      {

// 		//	foreach (var e in q1)
// 		//	{
// 		//		e.Set<NameComponent>(new("Kujo Jolyne"));
// 		//	}
// 		//}
// 		//w2.AddProcessor((Query<NameComponent> q) => SimpleForeach(q));
// 		//w2.Start();
// 	}

// 	[Benchmark]
// 	public void ForeachDeconstructQuery()
// 	{
// 		//for (int i = 0; i < 10; i++)
// 		//	w1.Update(false);

// 	}
// 	[Benchmark]
// 	public void ForeachQuery()
// 	{
// 		//for (int i = 0; i < 10; i++)
// 		//	w2.Update(false);
// 	}
// }

