using CommunityToolkit.HighPerformance.Helpers;
using SoftTouch.ECS.Processors;
using System.Runtime.InteropServices;

namespace SoftTouch.ECS.Scheduling;

public readonly struct GroupUpdater : IRefAction<ProcessorGroup>
{
    public void Invoke(ref ProcessorGroup item)
    {
        item.Update();
    }
}

public struct ProcessorStage
{
    public string Name { get; set; }
    public ProcessorGroupCollection ProcessorGroups { get; }

    //List<Task> actions;

    public ProcessorStage(string name)
    {
        Name = name;
        ProcessorGroups = new();
        ProcessorGroups.Add(new());
    }

    public void Add<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach (var group in ProcessorGroups)
            if (group.TryAdd(p))
                return;
        ProcessorGroups.Add(new ProcessorGroup().With(p));
    }
    public void Remove<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach (var group in ProcessorGroups)
            if (group.TryRemove(p))
                break;
    }

    public void Run(bool parallel = true)
    {
        if (ProcessorGroups.Length > 0)
        {
            if (parallel && ProcessorGroups.Length == 1 && ProcessorGroups[0].Count > 0)
            {
                ProcessorGroups[0].Update();
            }
            else if (parallel && ProcessorGroups.Length == 2 && ProcessorGroups[0].Count == 0)
            {
                ProcessorGroups[1].Update();
            }
            else if (parallel && ProcessorGroups.Length > 2)
                ParallelHelper.ForEach<ProcessorGroup,GroupUpdater>(ProcessorGroups.Memory);
            else
                foreach (var g in ProcessorGroups)
                    g.Update();
        }
    }

    public OrderedStage After(string other) => new() { Order = StageOrder.After, Stage = this, Other = other };
    public OrderedStage Before(string other) => new() { Order = StageOrder.Before, Stage = this, Other = other };
}