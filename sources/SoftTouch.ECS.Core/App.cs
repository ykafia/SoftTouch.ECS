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

    public App()
    {
        AppTime = new();
        Events = new();
        World = new(AppTime);
        World.Resources.Set(Events);
        Schedule = new(this);
        IsRunning = false;
    }

    public void Run(bool parallel = true)
    {
        IsRunning = true;
        while(IsRunning)
            Update(parallel);
    }

    public virtual void Update(bool parallel = true)
    {
        AppTime.Update();
        Events.Update();
        Schedule.Run(parallel);
        World.ApplyUpdates();
        SubApp?.Update(parallel);
        
    }
    public virtual void UpdateNoWorldUpdates(bool parallel = true)
    {
        AppTime.Update();
        Schedule.Run(parallel);
        //World.ApplyUpdates();
    }
}