using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

/// <summary>
/// A delegate that serves as extracting data from one world to another.
/// </summary>
/// <param name="main"></param>
/// <param name="sub"></param>
public delegate void ExtractDelegate(World main, World sub);

/// <summary>
/// A sub app running in parallel after the app.
/// </summary>
/// <param name="parent"></param>
public abstract class SubApp : App
{
    public App Parent { get; set;}
    public ExtractDelegate? ExtractFn { get; set; }

    public SubApp(App parent, List<Stage>? stages = null) : base(stages)
    {
        Parent = parent;
        World.Resources.Set(Parent.World);
    }

    public override void Update()
    {
        AppTime.Update();
        Schedule.Run();
    }

    public void Extract()
    {
        if(ExtractFn is not null)
            ExtractFn(Parent.World, World);
    }

    public SubApp SetStages<TStage>(ReadOnlySpan<SubStage<TStage>> subStages)
        where TStage : Stage
    {
        Schedule.SetStages(subStages);
        return this;
    }
}
