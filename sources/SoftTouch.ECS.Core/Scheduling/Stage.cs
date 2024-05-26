using CommunityToolkit.HighPerformance.Helpers;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.States;
using System.Runtime.InteropServices;

namespace SoftTouch.ECS.Scheduling;

public abstract class Stage()
{
    public App? App { get; set; }
    public ProcessorGroups ProcessorGroups { get; } = [new()];
    List<Task> updateTasks = new(10);
    public void Add<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach (var group in ProcessorGroups)
            if (group.TryAdd(p))
                return;
        ProcessorGroups.Add(new Group().With(p));
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
        if (ProcessorGroups.Count > 0)
        {
            if (parallel && ProcessorGroups.Count == 1 && ProcessorGroups[0].Count > 0)
            {
                ProcessorGroups[0].Update();
            }
            else if (parallel && ProcessorGroups.Count == 2 && ProcessorGroups[0].Count == 0)
            {
                var action = ProcessorGroups[1].StateEvent;
                if (action != null && App != null && !App.World.GetResource<WorldStates>().IsValid(action.Value))
                    return;
                else
                    ProcessorGroups[1].Update();
            }
            else if (parallel && ProcessorGroups.Count > 2)
            {
                updateTasks.Clear();
                foreach (var grp in ProcessorGroups)
                {
                    var action = grp.StateEvent;
                    if (action != null && App != null && !App.World.GetResource<WorldStates>().IsValid(action.Value))
                        continue;
                    else
                        updateTasks.Add(Task.Run(grp.Update));
                }
                Task.WhenAll();
            }
            else
                foreach (var g in ProcessorGroups)
                    g.Update();
        }
    }
}

public class Startup() : Stage();
public class Main() : Stage();
public class Extract() : Stage();

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