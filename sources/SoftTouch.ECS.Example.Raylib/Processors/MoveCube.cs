using Raylib_cs;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class MoveCube() : Processor<Query<Model>, Time>(null!)
{
    public override void Update()
    {
        var time = Query2;
        foreach(var e in Query1)
        {
            ref var cube = ref e.Get<Model>();
            cube.Transform *= Raymath.MatrixScale((float)Math.Sin(time.TotalElapsed.TotalSeconds) + 1, (float)Math.Sin(time.TotalElapsed.TotalSeconds) + 1, (float)Math.Sin(time.TotalElapsed.TotalSeconds) + 1); 
        }
    }
}