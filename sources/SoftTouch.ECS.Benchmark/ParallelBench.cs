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
using CommunityToolkit.HighPerformance.Helpers;

namespace SoftTouch.ECS.Benchmark;


public class MyProcess(int end)
{
    public int Update()
    {
        int result = 0;
        for(int i = 0; i < end; i++)
            result += i;
        return result;
    }
}

public struct RunMyProcess : IRefAction<MyProcess>
{
    public readonly void Invoke(ref MyProcess item)
    {
        item.Update();
    }
}
public struct FillMyArray(int[] array) : IAction
{
    int[] array = array;

    public void Invoke(int i)
    {
        array[i] = i;
    }
}

[MemoryDiagnoser]
[SimpleJob(launchCount: 1, warmupCount: 5, targetCount: 5)]
public class ParallelBench
{
    int[] ints;
    MyProcess[] processes;
    List<Task> tasks;
    List<ValueTask> valueTasks;



    public ParallelBench()
    {
        processes = [
            new(10),
            new(20),
            new(30),
            new(40),
            new(50),
            new(60),
            new(70),
            new(80),
            new(90),
            new(100),
        ];
        tasks = new(10);
        valueTasks = new(10);
        ints = new int[1000];
    }




    

    [Benchmark]
    public void CommunityParallelClearWith()
    {
        ParallelHelper.For(0, ints.Length, new FillMyArray(ints));
        ParallelHelper.For(0, ints.Length, new FillMyArray(ints));
        ParallelHelper.For(0, ints.Length, new FillMyArray(ints));
        
    }
    [Benchmark]
    public void CommunityParallelHelper()
    {
        ParallelHelper.ForEach<MyProcess, RunMyProcess>(processes.AsMemory());
        ParallelHelper.ForEach<MyProcess, RunMyProcess>(processes.AsMemory());
        ParallelHelper.ForEach<MyProcess, RunMyProcess>(processes.AsMemory());
    }
    [Benchmark]
    public void ParallelForEachT()
    {
        foreach (var p in processes)
            tasks.Add(Task.Run(p.Update));
        Task.WhenAll(tasks);
        tasks.Clear();
        foreach(var p in processes)
            tasks.Add(Task.Run(p.Update));
        Task.WhenAll(tasks);
        tasks.Clear();
        foreach (var p in processes)
            tasks.Add(Task.Run(p.Update));
        Task.WhenAll(tasks);
    }

}
