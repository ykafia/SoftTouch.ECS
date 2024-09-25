using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

/// <summary>
/// A sub app running in parallel after the app.
/// </summary>
/// <param name="parent"></param>
public abstract class SubApp(App parent, List<Stage>? stages = null) : App(stages)
{
    public App Parent { get; } = parent;
    public override void Update()
    {
        AppTime.Update();
        Schedule.Run();
    }

    public SubApp SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages)
        where TStage : Stage
    {
        Schedule.SetStages(subStages);
        return this;
    }
}
