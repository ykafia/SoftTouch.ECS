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

    public Scheduler Schedule { get; init; }

    public App()
    {
        World = new();
        Schedule = new();
        IsRunning = false;
    }


    public virtual void Update()
    {
        Schedule.Run();
        World.ApplyUpdates();
    }
}
