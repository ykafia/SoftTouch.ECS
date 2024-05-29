using System.ComponentModel;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public abstract record SubStage;
public abstract record SubStage<TStage> : SubStage
    where TStage : Stage
{
    readonly List<ProcessorGroup> groups = [];
    readonly List<Task> tasks = [];
    public void Update(bool parallel = true)
    {
        if (parallel)
        {
            tasks.Clear();
            foreach (var group in groups)
                tasks.Add(Task.Run(group.Update));
        }
        else
            foreach(var group in groups)
                group.Update();
    }

    public void Add(Processor processor)
    {
        if (groups.Count == 0)
        {
            groups.Add(new());
            groups[0].TryAdd(processor);
        }
        else
        {
            foreach (var group in groups)
                if (group.TryAdd(processor))
                    return;
            groups.Add(new());
            groups[^1].TryAdd(processor);
        }
    }
}
