using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public abstract class Stage
{
    public abstract void Update();
    public abstract bool TryAdd<TSubStage>(Processor processor) where TSubStage : SubStage;
    internal abstract void SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages) where TStage : Stage;
    
}
public abstract class Stage<T> : Stage
    where T : Stage<T>
{
    public bool Parallel { get; set; } = true;
    protected virtual List<SubStage<T>> SubStages { get; } = [];

    public override void Update()
    {
        foreach (var subStage in SubStages)
            subStage.Update(Parallel);
    }

    internal override void SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages)
    {
        SubStages.Clear();
        foreach (var subStage in subStages)
            if(subStage is SubStage<T> ss)
                SubStages.Add(ss);
    }

    public void InsertBefore<TSubStage, TBefore>()
        where TSubStage : SubStage<T>, new()
        where TBefore : SubStage<T>
    {
        for (int i = 0; i < SubStages.Count; i++)
        {
            if (SubStages[i] is TBefore)
            {
                SubStages.Insert(i, new TSubStage());
                return;
            }
        }
    }

    public void InsertAfter<TSubStage, TAfter>()
        where TSubStage : SubStage<T>, new()
        where TAfter : SubStage<T>
    {
        for (int i = 0; i < SubStages.Count; i++)
        {
            if (SubStages[i] is TAfter)
            {
                SubStages.Insert(i + 1, new TSubStage());
                return;
            }
        }
    }

    public override bool TryAdd<TSubStage>(Processor processor)
    {
        foreach(var subStage in SubStages)
            if(subStage.GetType() == typeof(TSubStage))
            {
                subStage.Add(processor);
                return true;
            }
        return false;
    }

    public sealed override string ToString()
    {
        return $"{GetType().Name}[{string.Join(", ", SubStages)}]";
    }
}

public sealed class Main : Stage<Main>
{
    bool started = false;
    protected override List<SubStage<Main>> SubStages { get; } = [
        new PreStartup(),
        new Startup(),
        new PostStartup(),
        new First(),
        new PreUpdate(),
        new Update(),
        new PostUpdate(),
        new Last()
    ];

    public override void Update()
    {
        if(!started)
        {
            foreach (var stage in SubStages)
                if (stage is StartupBase)
                    stage.Update(Parallel);
            started = true;
        }
        foreach(var stage in SubStages)
            if (stage is not StartupBase)
                stage.Update(Parallel);
    }
}