using Raylib_cs;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class MoveLight() : Processor<Query<Write<Light>>, Time>(null!)
{
    public override void Update()
    {
        var time = Query2;
        foreach (var e in Query1)
        {
            ref var light = ref e.GetRef<Light>();
            light.Position = light.Position with { Y = (float)Math.Cos(time.TotalElapsed.TotalSeconds) + 1 };
        }
    }
}