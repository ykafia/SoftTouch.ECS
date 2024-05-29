using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

/// <summary>
/// A sub app running in parallel after the app.
/// </summary>
/// <param name="parent"></param>
public class SubApp(App parent) : App
{
    public App Parent { get; } = parent;
    public override void Update(bool parallel = true)
    {
        AppTime.Update();
        Schedule.RunExtract();
        Schedule.Run(parallel);
    }

    public SubApp SetStages<TStage>(ReusableList<SubStage<TStage>> subStages)
        where TStage : Stage
    {
        Schedule.SetStages(subStages);
        return this;
    }
}

public class RenderApp(App parent) : SubApp(parent);
