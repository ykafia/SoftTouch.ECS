using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS;

public class SubApp : App
{
    public override void Update(bool parallel = true)
    {
        AppTime.Update();
        Schedule.Run<Extract>(false);
        Schedule.Run(parallel);
    }
}
