using SoftTouch.ECS.NewProcessors;
using System.Diagnostics.CodeAnalysis;

namespace SoftTouch.ECS.NewProcessors;

public class Scheduler
{
    public World World { get; }

    public SortedList<string, List<NewProcessor>> Schedules;
    public SortedList<int, string> Order;

    public Scheduler(World world)
    {
        World = world;
        Schedules = new();
        Order = new();
    }

    public void Register<T>([NotNull] string schedule)
        where T : NewProcessor, new()
    {
        if(Schedules.TryGetValue(schedule, out var list))
        {
            list.Add(NewProcessor.Create<T>(World));
            Order.Add(Order.Count, schedule);
        }
        else
        {
            Schedules.Add(schedule, new());
            Register<T>(schedule);
        }
    }
}