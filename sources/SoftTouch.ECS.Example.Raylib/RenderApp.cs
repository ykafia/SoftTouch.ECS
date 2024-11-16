namespace SoftTouch.ECS.Example.Rlib;


public class RenderApp(App parent) : SubApp(parent, [new Extraction(), new Render()])
{
    public override void Update()
    {
        base.Update();
        // World is cleared every time
        World.Clear();
    }
}