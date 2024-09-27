using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS.Example.Rlib;


public class RenderApp(App parent) : SubApp(parent, [new Extraction(), new Render()]);