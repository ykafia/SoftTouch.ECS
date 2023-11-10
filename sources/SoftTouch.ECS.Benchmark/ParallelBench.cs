using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using System.Linq;
using SoftTouch.ECS.Shared.Processors;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SoftTouch.ECS.Benchmark;

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 5)]
public class ParallelBench
{

    List<Task> tasks;
    List<ValueTask> valueTasks;

    public ParallelBench()
    {
        tasks = new();
        valueTasks = new();
        foreach(var n in Enumerable.Range(0,10))
        {
            tasks.Add(new(TaskCall));
            valueTasks.Add(ValueTaskCall());
        }
    }

    public static async ValueTask ValueTaskCall()
    {
        await Task.Delay(100);
    }

    public static async void TaskCall()
    {
        await Task.Delay(100);
    }


    [Benchmark]
    public void ParallelForEachVT()
    {
        Parallel.ForEachAsync(valueTasks, async (vt, ct) => await vt).Wait();
    }
    [Benchmark]
    public void ParallelForEachT()
    {
        Parallel.ForEachAsync(tasks, async (t, ct) => await t).Wait();
    }
    //[Benchmark]
    //public void WaitAll()
    //{
    //}
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
