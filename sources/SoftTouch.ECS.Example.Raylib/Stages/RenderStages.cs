using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS.Example.Rlib;

public record StartRender : SubStage<Render>;
public record MainRender : SubStage<Render>;
public record EndRender : SubStage<Render>;