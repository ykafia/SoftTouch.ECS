﻿using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;
namespace SoftTouch.ECS;

public partial class App
{
    public App AddProcessor(Processor processor, string name = "Main")
    {
        processor.World = World;
        AddProcessors(name, processor);
        return this;
    }
    public App AddStartupProcessor(Processor processor)
    {
        processor.World = World;
        processor.RunAndDisable = true;
        AddProcessors("Startup", processor);
        return this;
    }
    public App AddProcessor<T>(string name = "Main")
        where T : Processor, new()
    {
        AddProcessors(name, new T() { World = World });
        return this;
    }

    public App AddStartupProcessor<T>()
        where T : Processor, new()
    {
        AddProcessors("Startup", new T() { World = World, RunAndDisable = true});
        return this;
    }

    public App AddProcessor<Q>(UpdaterFunc<Q> func, string name = "Main")
        where Q : struct, IWorldQuery
    {
        AddProcessors(name, new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func, string name = "Main")
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        AddProcessors(name, new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func, string name = "Main")
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        AddProcessors(name, new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func, string name = "Main")
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        AddProcessors(name, new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q>(UpdaterFunc<Q> func)
        where Q : struct, IWorldQuery
    {
        AddProcessors("Startup", new DelegateProcessor<Q>(func) { World = World, RunAndDisable = true });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        AddProcessors("Startup", new DelegateProcessor<Q1, Q2>(func) { World = World, RunAndDisable = true });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        AddProcessors("Startup", new DelegateProcessor<Q1, Q2, Q3>(func) { World = World, RunAndDisable = true });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        AddProcessors("Startup", new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World, RunAndDisable = true });
        return this;
    }

}
