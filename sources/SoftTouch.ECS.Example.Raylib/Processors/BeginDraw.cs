using Raylib_cs;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Example.Rlib;


public class BeginDraw() : Processor(null!)
{
    public override void Update()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.LIGHTGRAY);
    }
}
public class EndDraw() : Processor(null!)
{
    public override void Update()
    {
        Raylib.EndDrawing();
    }
}