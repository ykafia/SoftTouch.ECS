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

public abstract record Stage()
{
    public ProcessorGroupCollection ProcessorGroups { get; } = [new()];
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
                ParallelHelper.ForEach<ProcessorGroup, GroupUpdater>(ProcessorGroups.Memory);
            else
                foreach (var g in ProcessorGroups)
                    g.Update();
        }
    }
}

public record Startup() : Stage();
public record Main() : Stage();
public record Extract() : Stage();

public static class StageExtensions
{
    public static OrderedStage<TSelf, TOther> After<TSelf, TOther>(this TSelf stage)
        where TSelf : Stage
        where TOther : Stage
            => new() { Order = StageOrder.After };
    public static OrderedStage<TSelf, TOther> Before<TSelf, TOther>(this TSelf stage)
        where TSelf : Stage
        where TOther : Stage
            => new() { Order = StageOrder.Before };
}

// public struct ProcessorStage(string name)
// {
//     public string Name { get; set; } = name;
//     public ProcessorGroupCollection ProcessorGroups { get; } = [new()];

//     public readonly void Add<TProcessor>(TProcessor p)
//         where TProcessor : Processor
//     {
//         foreach (var group in ProcessorGroups)
//             if (group.TryAdd(p))
//                 return;
//         ProcessorGroups.Add(new ProcessorGroup().With(p));
//     }
//     public readonly void Remove<TProcessor>(TProcessor p)
//         where TProcessor : Processor
//     {
//         foreach (var group in ProcessorGroups)
//             if (group.TryRemove(p))
//                 break;
//     }

//     public readonly void Run(bool parallel = true)
//     {
//         if (ProcessorGroups.Length > 0)
//         {
//             if (parallel && ProcessorGroups.Length == 1 && ProcessorGroups[0].Count > 0)
//             {
//                 ProcessorGroups[0].Update();
//             }
//             else if (parallel && ProcessorGroups.Length == 2 && ProcessorGroups[0].Count == 0)
//             {
//                 ProcessorGroups[1].Update();
//             }
//             else if (parallel && ProcessorGroups.Length > 2)
//                 ParallelHelper.ForEach<ProcessorGroup, GroupUpdater>(ProcessorGroups.Memory);
//             else
//                 foreach (var g in ProcessorGroups)
//                     g.Update();
//         }
//     }

//     public OrderedStage After(string other) => new() { Order = StageOrder.After, Stage = this, Other = other };
//     public OrderedStage Before(string other) => new() { Order = StageOrder.Before, Stage = this, Other = other };
// }