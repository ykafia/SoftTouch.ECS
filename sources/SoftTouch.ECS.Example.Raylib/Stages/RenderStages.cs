using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS.Example.Rlib;

public record MainRender : SubStage<Render>;
public record RenderEnd : SubStage<Render>;