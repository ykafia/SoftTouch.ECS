﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

public partial class App
{
    public AppTime AppTime { get; }
    public World World { get; }

    public bool IsRunning { get; private set; }

    public Scheduler Schedule { get; init; }

    public App()
    {
        AppTime = new();
        World = new(AppTime);
        Schedule = new();
        IsRunning = false;
    }


    public virtual void Update(bool parallel = true)
    {
        AppTime.Update();
        Schedule.Run(parallel);
        World.ApplyUpdates();
    }
    public virtual void UpdateNoWorldUpdates(bool parallel = true)
    {
        AppTime.Update();
        Schedule.Run(parallel);
        //World.ApplyUpdates();
    }
}