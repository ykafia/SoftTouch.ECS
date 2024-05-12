using System.Collections;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;
namespace SoftTouch.ECS;

public partial class App
{
    public App AddBundle<TStage, TBundle>() 
        where TStage : Stage, new()
        where TBundle : struct, IProcessorBundle
        => new TBundle();

    public App SetStages(ReadOnlySpan<Stage> stages)
    {
        Schedule.Clear();
        foreach (var stage in stages)
            if (stage is Extract)
                throw new Exception("Stages must not contain the stage Extract");
            else if (stage is Startup)
                throw new Exception("Stages must not contain the stage Startup");
            else
                Schedule.Add(stage);
        return this;
    }


    public App AddProcessors<TStage>(ReadOnlySpan<Processor> processors)
        where TStage : Stage, new()
    {
        foreach(var p in processors)
            AddProcessor<TStage>(p);
        return this;
    }

    public App AddProcessor<TStage>(Processor processor)
        where TStage : Stage, new()
    {
        processor.World = World;
        AddProcessors<TStage>(processor);
        return this;
    }

    public App AddProcessor(Processor processor)
    {
        processor.World = World;
        AddProcessors<Main>(processor);
        return this;
    }
    public App AddStartupProcessor(Processor processor)
    {
        processor.World = World;
        AddProcessors<Startup>(processor);
        return this;
    }
    public App AddProcessor<T>()
        where T : Processor, new()
    {
        AddProcessors<Main>(new T() { World = World });
        return this;
    }

    public App AddStartupProcessor<T>()
        where T : Processor, new()
    {
        AddProcessors<Startup>(new T() { World = World });
        return this;
    }
    public App AddProcessor<Q>(UpdaterFunc<Q> func)
            where Q : struct, IWorldQuery
    {
        AddProcessors<Main>(new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddProcessor<TStage, Q>(UpdaterFunc<Q> func)
        where TStage : Stage, new()
        where Q : struct, IWorldQuery
    {
        AddProcessors<TStage>(new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        AddProcessors<Main>(new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddProcessor<TStage, Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where TStage : Stage, new()
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        AddProcessors<TStage>(new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        AddProcessors<Main>(new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddProcessor<TStage, Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where TStage : Stage, new()
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        AddProcessors<TStage>(new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddProcessor<TStage, Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where TStage : Stage, new()
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        AddProcessors<TStage>(new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }
    public App AddProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
       where Q1 : struct, IWorldQuery
       where Q2 : struct, IWorldQuery
       where Q3 : struct, IWorldQuery
       where Q4 : struct, IWorldQuery
    {
        AddProcessors<Main>(new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q>(UpdaterFunc<Q> func)
        where Q : struct, IWorldQuery
    {
        AddProcessors<Startup>(new DelegateProcessor<Q>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
    {
        AddProcessors<Startup>(new DelegateProcessor<Q1, Q2>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
    {
        AddProcessors<Startup>(new DelegateProcessor<Q1, Q2, Q3>(func) { World = World });
        return this;
    }
    public App AddStartupProcessor<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
    {
        AddProcessors<Startup>(new DelegateProcessor<Q1, Q2, Q3, Q4>(func) { World = World });
        return this;
    }
}