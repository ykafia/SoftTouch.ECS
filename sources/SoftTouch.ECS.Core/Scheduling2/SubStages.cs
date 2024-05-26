namespace SoftTouch.ECS.Scheduling2;


public abstract record SubStage;
public abstract record SubStage<TStage> : SubStage
    where TStage : Stage
{
    readonly List<ProcessorGroup> groups = [];
    readonly List<Task> tasks = [];
    public void Update()
    {
        tasks.Clear();
        foreach (var group in groups)
            tasks.Add(Task.Run(group.Update));
    }
}
