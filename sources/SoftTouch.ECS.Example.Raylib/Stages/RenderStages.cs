using SoftTouch.ECS.Scheduling;

namespace SoftTouch.ECS.Example.Rlib;


public record Extract : SubStage<Extraction>;

public record Extraction : Stage<Extraction>
{
    protected override List<SubStage<Extraction>> SubStages { get; } = [
        new Extract()
    ];

    public override void Update()
    {
        foreach(var stage in SubStages)
            stage.Update(false);
    }
}

public record StartRender : SubStage<Render>;
public record MainRender : SubStage<Render>;
public record EndRender : SubStage<Render>;

public record Render : Stage<Render>
{
    protected override List<SubStage<Render>> SubStages { get; } = [
        new StartRender(),
        new MainRender(),
        new EndRender()
    ];

    public override void Update()
    {
        foreach(var stage in SubStages)
            stage.Update(false);
    }
}