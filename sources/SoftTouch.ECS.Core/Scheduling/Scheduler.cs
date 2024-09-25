using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public class Scheduler(App app, List<Stage>? stages = null)
{
    public App App { get; } = app;
    readonly List<Stage> stages = stages ?? [new Main()];

    public void Run()
    {
        foreach (var stage in stages)
            stage.Update();
    }

    public void Add<TStage>(ReadOnlySpan<Processor> processors)
        where TStage : SubStage
    {
        foreach(var processor in processors)
            Add<TStage>(processor);
    }

    public void Add<TSubStage>(Processor processor)
        where TSubStage : SubStage
    {
        foreach( var stage in stages)
            if (stage.TryAdd<TSubStage>(processor))
                return;
    }

    /// <summary>
    /// Set sub stages of specified stage and dispose of the ReusableList used as parameter.
    /// </summary>
    /// <typeparam name="TStage"></typeparam>
    /// <param name="subStages">ReadOnlyList of sub stages to be set in the Stage specified. The list is disposed after calling the function</param>
    internal void SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages)
        where TStage : Stage
    {
        foreach( var stage in stages)
            if (stage is TStage)
                stage.SetStages(subStages);
    }

    public override string ToString()
    {
        return string.Join(", ", stages.Select(x => x.ToString()));
    }

}