using System.ComponentModel;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public abstract class SubStage
{
    public abstract Type StageType { get; }
}
public abstract class SubStage<TStage> : SubStage
    where TStage : Stage
{
    public override Type StageType => typeof(TStage);
    internal List<ProcessorGroup> groups = [];
    readonly List<Task> tasks = [];
    public virtual void Update(bool parallel = true)
    {
        if (parallel && groups.Count > 1)
        {
            tasks.Clear();
            foreach (var group in groups)
                if(group.Count > 0)
                    tasks.Add(Task.Run(group.Update));
            Task.WhenAll(tasks);
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

    public sealed override string ToString()
    {
        return $"{GetType().Name} [{string.Join(", ", groups)}]";
    }
}
