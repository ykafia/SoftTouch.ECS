namespace SoftTouch.ECS;

public class SubApp : App
{
    public override void Update(bool parallel = true)
    {
        AppTime.Update();
        Schedule.RunExtract();
        Schedule.Run(parallel);
    }
}
