using CommunityToolkit.HighPerformance.Helpers;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public readonly struct GroupUpdater : IAction
{
    private readonly List<ProcessorGroup> _groups;
    public GroupUpdater(List<ProcessorGroup> groups)
    {
        _groups = groups;
    }
    public void Invoke(int i)
    {
        _groups[i].Update();
    }
}

public struct ProcessorStage
{
    public string Name { get; set; }
    public List<ProcessorGroup> ProcessorGroups { get; }
    List<Task> tasks;

    public ProcessorStage(string name)
    {
        Name = name;
        ProcessorGroups = new()
        {
            new()
        };
        tasks = new(8);
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
        if (ProcessorGroups.Count > 0)
        {
            if (parallel && ProcessorGroups.Count >= 2 && ProcessorGroups[0].Count > 0)
                ParallelHelper.For(..ProcessorGroups.Count, new GroupUpdater(ProcessorGroups));
            else
                foreach (var g in ProcessorGroups)
                    g.Update();
        }
    }

    public OrderedStage After(string other) => new() { Order = StageOrder.After, Stage = this, Other = other };
    public OrderedStage Before(string other) => new() { Order = StageOrder.Before, Stage = this, Other = other };
}