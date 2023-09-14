using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Shared.Processors;
using System;

namespace SoftTouch.ECS.Benchmark;

[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class QueryBench
{
    World w1;
    World w2;
    World w3;

    public QueryBench()
    {
        //w1 = new();

        //w1.Commands.Spawn(new NameComponent() { Name = "Martha" }).With<HealthComponent>();
        //w1.Commands.Spawn(new NameComponent() { Name = "Martha" }, new HealthComponent());
        //w1.Commands.Spawn(new NameComponent() { Name = "Martha" },default(int));
        //w1.Commands.Spawn(new NameComponent() { Name = "Martha" }, new HealthComponent(),(1, 5));
        //w1.Commands.Spawn(new NameComponent() { Name = "Martha" },new HealthComponent());

        ////w1.AddProcessor<OtherNameProcessor>();
        //w1.Start();

        //w2 = new();

        //w2.Commands.Spawn(
        //new NameComponent("Martha"));
        //w2.Commands.Spawn(
        //new NameComponent("Martha" ),
        //new HealthComponent());
        //w2.Commands.Spawn(
        //new NameComponent("Martha"), default(int));
        //w2.Commands.Spawn(
        //new NameComponent("Martha"),
        //new HealthComponent(),
        //(1, 5));
        //w2.Commands.Spawn(
        //new NameComponent("Martha"),
        //new HealthComponent());

        ////w2.AddProcessor<NameProcessor>();
        //w2.Start();

        //w3 = new();

        //w3.Commands.Spawn(
        //new NameComponent() { Name = "Martha" });
        //w3.Commands.Spawn(
        //new NameComponent() { Name = "Martha" },
        //new HealthComponent());
        //w3.Commands.Spawn(
        //new NameComponent() { Name = "Martha" },
        //default(int));
        //w3.Commands.Spawn(
        //new NameComponent() { Name = "Martha" },
        //new HealthComponent(),
        //(1, 5));
        //w3.Commands.Spawn(
        //new NameComponent() { Name = "Martha" },
        //new HealthComponent());

        //static void UpdateName(Query<NameComponent> q1)
        //{
        //    var iter = q1.GetEnumerator();
        //    while (iter.MoveNext())
        //    {
        //        iter.Current.Set<NameComponent>(new("Kujo Jolyne"));
        //    }
        //}

        //w3.AddProcessor((Query<NameComponent> q1) => UpdateName(q1));
        //w3.Start();
    }

    [Benchmark]
    public void QueryArchetypesWithLock()
    {
        //for (int i = 0; i < 10; i++)
        //    w1.Update(false);

    }
    [Benchmark]
    public void QueryComponentsWithoutLock()
    {
        //for (int i = 0; i < 10; i++)
        //    w2.Update(false);
    }
    [Benchmark]
    public void QueryIteratorWithLock()
    {
        //for (int i = 0; i < 10; i++)
        //    w3.Update(false);
    }
}

