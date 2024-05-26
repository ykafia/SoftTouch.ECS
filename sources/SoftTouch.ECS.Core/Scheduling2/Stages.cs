namespace SoftTouch.ECS.Scheduling2;

public abstract record Stage;

public abstract record Stage<T> : Stage
    where T : Stage<T>
{
    protected virtual List<SubStage<T>> SubStages { get; } = [];
    public void Update()
    {
        foreach (var subStage in SubStages)
            subStage.Update();
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
