using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;

public class RenderApp : SubApp
{
    public RenderApp(App parent) : base(parent, [new Extract()])
}