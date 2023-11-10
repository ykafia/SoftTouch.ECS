using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Linq;
using SoftTouch.ECS.Shared.Processors;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Benchmark;

public class StartupProcessor : Processor<Resource<WorldCommands>>
{
    public StartupProcessor() : base(null!)
    {

    }
    public override void Update()
    {
        Random rand = new Random();
        WorldCommands commands = Query;
        for (int i = 0; i < 1000; i++)
        {
            commands.Spawn(rand.Next(1, 100), new NameComponent($"john n°{i}"), 0f);
        }
    }
}

public class WriteAge : Processor<Query<Write<int>>>
{
    public WriteAge() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            entity.Set(entity.Get<int>() + 1);
        }
    }
}
public class SayHello : Processor<Query<Read<NameComponent>, Write<float>>>
{
    public SayHello() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            entity.Set<float>(entity.Get<NameComponent>().Name.Length);
        }
    }
}

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 5)]
public class GameBench
{
    App app;
    public GameBench()
    {
        app =
           new App()
           .AddStartupProcessor<StartupProcessor>()
           .AddProcessor<SayHello>()
           .AddProcessor<WriteAge>();
        app.Update();
    }


    [Benchmark]
    public void SingleParallelUpdate()
    {
        app.Update();
    }
    [Benchmark]
    public void TenParallelUpdate()
    {
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
        app.Update();
    }
    //[Benchmark]
    //public void SingleParallelUpdateNoWUpdate()
    //{
    //    app.UpdateNoWorldUpdates();
    //}
    //[Benchmark]
    //public void SingleSyncUpdate()
    //{
    //    app.Update(false);
    //}

}
