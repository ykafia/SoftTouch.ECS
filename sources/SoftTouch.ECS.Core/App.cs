using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

public partial class App
{
    public AppTime AppTime { get; }
    public World World { get; }

    public bool IsRunning { get; private set; }

    public Scheduler Schedule { get; }
    public EventsResource Events { get; }

    public SubApp? SubApp { get; set; }

    public App(List<Stage>? stages = null)
    {
        AppTime = new();
        Events = new();
        World = new(AppTime);
        World.Resources.Set(Events);
        Schedule = new(this, stages ?? [new Main()]);
        IsRunning = false;
    }

    public void Run()
    {
        IsRunning = true;
        while(IsRunning)
            Update();
    }

    public virtual void Update()
    {
        AppTime.Update();
        Events.Update();
        Schedule.Run();
        World.ApplyUpdates();
        SubApp?.Update();
        
    }
    public virtual void UpdateNoWorldUpdates()
    {
        AppTime.Update();
        Schedule.Run();
        //World.ApplyUpdates();
    }
}