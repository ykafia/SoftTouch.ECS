using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public partial class App
{
    public App AddProcessor(Processor processor)
    {
        processor.World = World;
        Processors.Add(processor);
        return this;
    }
    public App AddStartupProcessor(Processor processor)
    {
        processor.World = World;
        StartupProcessors.Add(processor);
        return this;
    }
    public App AddProcessor<T>()
        where T : Processor, new()
    {
        Processors.Add(new T() { World = World });
        return this;
    }

    public App AddStartupProcessor<T>()
        where T : Processor, new()
    {
        StartupProcessors.Add(new T() { World = World });
        return this;
    }

    public App AddProcessor<Q>(UpdaterFunc<Q> func)
        where Q : struct, IWorldQuery
    {
        Processors.Add(new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        Processors.Add(new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        Processors.Add(new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        Processors.Add(new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q>(UpdaterFunc<Q> func)
        where Q : struct, IWorldQuery
    {
        StartupProcessors.Add(new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        StartupProcessors.Add(new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        StartupProcessors.Add(new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        StartupProcessors.Add(new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }

}
