namespace SoftTouch.ECS.Scheduling;


public enum StageOrder
{
    Before,
    After
}

public struct OrderedStage
{
    public ProcessorStage Stage { get; set; }
    public string Other { get; set; }
    public StageOrder Order { get; set; }
}