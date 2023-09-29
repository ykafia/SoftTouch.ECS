using SoftTouch.ECS.Processors;
using System.Diagnostics.CodeAnalysis;

namespace SoftTouch.ECS.Scheduling;
public class Scheduler
{
    public World World { get; }

    public SortedList<ProcessorStage, List<Processor>> Schedules;
    public List<string> Order;

    public Scheduler(World world)
    {
        World = world;
        Schedules = new();
        Order = new();
    }

    public void AddProcessors(string stage, params Processor[] processors)
    {

    }
}