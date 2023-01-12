using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using SoftTouch.ECS;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Shared.Processors;

namespace SoftTouch.ECS.Benchmark;

[MemoryDiagnoser]
[SimpleJob(launchCount: 3, warmupCount: 10, targetCount: 15)]
public class QueryBench
{
    World w;

    public QueryBench()
    {
        w = new();

        w.CreateEntity()
        .With(new NameComponent(){Name = "Martha"});
        w.CreateEntity()
        .With(new NameComponent(){Name = "Martha"})
        .With(new HealthComponent());
        w.CreateEntity()
        .With(new NameComponent(){Name = "Martha"})
        .With<int>();
        w.CreateEntity()
        .With(new NameComponent(){Name = "Martha"})
        .With(new HealthComponent())
        .With((1,5));
        w.CreateEntity()
        .With(new NameComponent(){Name = "Martha"})
        .With(new HealthComponent());

        w.Add<NameProcessor>();
        w.Start();
    }

    [Benchmark]
    public void QueryArchetypes()
    {
        foreach( var arch in w.QueryArchetypes(typeof(NameComponent)))
        {
            var span = arch.GetComponentSpan<NameComponent>();
            for(int i = 0; i < span.Length; i++)
            {
                span[i].Name = "Batman";
            }
        }
    }
    [Benchmark]
    public void QueryComponents()
    {
        w.Update();
    }
}

