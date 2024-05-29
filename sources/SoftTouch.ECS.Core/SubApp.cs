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
}

public class RenderApp(App parent) : SubApp(parent);
