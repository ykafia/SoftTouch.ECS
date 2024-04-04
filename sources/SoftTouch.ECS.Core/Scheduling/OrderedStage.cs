namespace SoftTouch.ECS.Scheduling;


public enum StageOrder
{
    Before,
    After
}

public struct OrderedStage<TStage, TOther>
    where TStage : Stage
    where TOther : Stage
{
    public StageOrder Order { get; set; }
}