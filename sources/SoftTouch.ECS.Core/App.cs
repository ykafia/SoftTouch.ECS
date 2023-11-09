using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

public partial class App
{
    public World World { get; init; }

    public bool IsRunning { get; private set; }
    public List<Processors.Processor> Processors { get; init; }
    public List<Processors.Processor> StartupProcessors { get; init; }

    public Scheduler Schedule { get; init; }

    public App()
    {
        World = new();
        Processors = new();
        Schedule = new();
        StartupProcessors = new();
        IsRunning = false;
    }

    public void Start()
    {
        IsRunning = true;
        foreach (var p in StartupProcessors)
            p.Update();
        World.ApplyUpdates();
    }


    public virtual void Update()
    {
        if (!IsRunning)
            Start();

        foreach (var p in Processors)
            p.Update();
        World.ApplyUpdates();
    }
}
