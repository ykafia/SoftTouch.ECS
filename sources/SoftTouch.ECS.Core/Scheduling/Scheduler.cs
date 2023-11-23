using System.Diagnostics.Tracing;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public class Scheduler
{
    public ProcessorStageCollection Stages { get; }

    public Scheduler()
    {
        Stages = new();
    }

    public void Run(bool parallel = true)
    {
        foreach (var stage in Stages)
            stage.Run(parallel);
    }

    public void Add(in ProcessorStage stage)
    {
        foreach(var s in Stages)
            if(s.Name == stage.Name)
            {
                foreach(var g in stage.ProcessorGroups)
                    foreach(var p in g)
                        s.Add(p);
            }
        Stages.Add(stage);
    }
    public void Add(in MergeStage stage)
    {
        foreach (var s in Stages)
            if (s.Name == stage.Name)
            {
                foreach(var processor in stage.Processors.Span)
                    s.Add(processor);
                return;
            }
        var newStage = new ProcessorStage("Main");
        foreach (var processor in stage.Processors.Span)
            newStage.Add(processor);
        Add(newStage);
    }
    public void Add(in OrderedStage stage)
    {
        foreach (var s in Stages)
            if (s.Name == stage.Stage.Name)
                throw new Exception($"Stage with name {stage.Stage.Name} already exists");
        for (int i = 0; i < Stages.Count; i++)
        {
            if(Stages[i].Name == stage.Other)
            {
                Stages.Insert(
                    stage.Order switch {
                        StageOrder.After => i+1,
                        StageOrder.Before => i,
                        _ => throw new NotImplementedException()
                    }, 
                    stage.Stage
                );
            }
        }
    }
    public void Add<TProcessor>(TProcessor p, string to)
        where TProcessor : Processor
    {
        foreach(var s in Stages)
        if(s.Name == to)
        {
            s.Add(p);
            return;
        }
        throw new Exception("Processor could not be added");
    }
}