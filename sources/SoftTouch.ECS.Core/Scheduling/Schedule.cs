using SoftTouch.ECS.Processors;
using System.Diagnostics.CodeAnalysis;

namespace SoftTouch.ECS.Scheduling;

public enum Stages
{
    Input,
    Update,
    Transformation
}


public class Scheduler
{
    public World World { get; }

    public SortedList<string, List<Processors.Processor>> Schedules;
    public List<string> Order;

    public Scheduler(World world)
    {
        World = world;
        Schedules = new();
        Order = new();
    }

    public void Register<T>([NotNull] string stage)
        where T : Processors.Processor, new()
    {
        if (Schedules.TryGetValue(stage, out var list))
        {
            list.Add(Processors.Processor.Create<T>(World));
            Order.Add(stage);
        }
        else
        {
            Schedules.Add(stage, new());
            Register<T>(stage);
        }
    }
}