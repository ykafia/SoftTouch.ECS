using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public abstract record Stage;

public abstract record Stage<T> : Stage
    where T : Stage<T>
{
    protected virtual List<SubStage<T>> SubStages { get; } = [];
    public void Update(bool parallel = true)
    {
        foreach (var subStage in SubStages)
            subStage.Update(parallel);
    }

    internal void SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages)
        where TStage : T
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

    public bool TryAdd<TSubStage>(Processor processor)
        where TSubStage : SubStage
    {
        foreach(var subStage in SubStages)
            if(subStage.GetType() == typeof(TSubStage))
            {
                subStage.Add(processor);
                return true;
            }
        return false;
    }
}

public record Main : Stage<Main>
{
    protected override List<SubStage<Main>> SubStages { get; } = [
        new First(),
        new PreUpdate(),
        new Update(),
        new PostUpdate(),
        new Last()
    ];
}


public record Extract : Stage<Extract>;
public record Render : Stage<Render>;
