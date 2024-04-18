using System.Diagnostics.Tracing;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public class Scheduler
{
    public StageCollection Stages { get; } = [];

    public void Run(bool parallel = true)
    {
        foreach (var stage in Stages)
            stage.Run(parallel);
    }
    public void Run<TStage>(bool parallel = true)
        where TStage : Stage
        => Stages.Get<TStage>().Run(parallel);

    public void Add(in Stage stage)
    {
        foreach (var s in Stages)
            if (s.GetType() == stage.GetType())
                foreach (var g in stage.ProcessorGroups)
                    foreach (var p in g)
                        s.Add(p);
        Stages.Add(stage);
    }
    public void Add<TStage>(in MergeStage<TStage> stage)
        where TStage : Stage, new()
    {
        foreach (var s in Stages)
            if (s is TStage)
            {
                foreach (var processor in stage.Processors.Span)
                    s.Add(processor);
                return;
            }
        var newStage = new TStage();
        foreach (var processor in stage.Processors.Span)
            newStage.Add(processor);
        Add(newStage);
    }
    public void Add<TStage, TOther>(in OrderedStage<TStage, TOther> stage)
        where TStage : Stage
        where TOther : Stage
    {
        foreach (var s in Stages)
            if (s is TStage)
                throw new Exception($"Stage with name {typeof(TStage)} already exists");

        if (typeof(TOther) == typeof(Startup))
            Stages.Insert(
                stage.Order switch
                {
                    StageOrder.After => 0,
                    StageOrder.Before => throw new Exception("Cannot put a stage before startup"),
                    _ => throw new NotImplementedException()
                },
                Stages.Get<Startup>()
            );
        else if (typeof(TOther) == typeof(Extract))
            Stages.Insert(
                stage.Order switch
                {
                    StageOrder.After => throw new Exception("Cannot put a stage after extract"),
                    StageOrder.Before => Stages.Count,
                    _ => throw new NotImplementedException()
                },
                Stages.Get<Extract>()
            );
        else
        {
            for (int i = 0; i < Stages.Count; i++)
            {
                if (Stages[i] is TOther)
                {
                    Stages.Insert(
                        stage.Order switch
                        {
                            StageOrder.After => i + 1,
                            StageOrder.Before => i,
                            _ => throw new NotImplementedException()
                        },
                        Stages.Get<TOther>()
                    );
                }
            }
        }
    }
    public void Add<TProcessor>(TProcessor p, string to)
        where TProcessor : Processor
    {
        foreach (var s in Stages)
            if (s.GetType() == to.GetType())
            {
                s.Add(p);
                return;
            }
        throw new Exception("Processor could not be added");
    }

    public void Clear()
    {
        Stages.Clear();
    }
}