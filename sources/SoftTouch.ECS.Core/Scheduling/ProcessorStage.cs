using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public struct ProcessorStage
{
    public string Name { get; set; }
    public List<ProcessorGroup> ProcessorGroups { get; }
    List<Task> tasks;

    public ProcessorStage(string name)
    {
        Name = name;
        ProcessorGroups = new();
        tasks = new(8);
    }

    public void Add<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach(var group in ProcessorGroups)
            if(group.TryAdd(p))
                break;
    }
    public void Remove<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach(var group in ProcessorGroups)
            if(group.TryRemove(p))
                break;
    }

    public void Update()
    {
        tasks.Clear();
        foreach(var g in ProcessorGroups)
            tasks.Add(Task.Run(g.Update));
        Task.WhenAll(tasks).Wait();
    }
}