using Raylib_cs;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class CubeProcessor() : Processor<Query<Read<Model>, Write<Transform>>, Time>(null!)
{
    public override void Update()
    {
        var time = Query2;
        foreach(var e in Query1)
        {
            ref var transform = ref e.GetRef<Transform>();
            transform.Scale = new((float)Math.Sin(time.TotalElapsed.TotalSeconds) + 1);
        }
    }
}