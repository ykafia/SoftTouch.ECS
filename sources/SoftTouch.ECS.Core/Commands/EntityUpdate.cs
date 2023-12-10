namespace SoftTouch.ECS;


public enum EntityUpdateKind
{
    ComponentUpdate,
    Spawn,
    Despawn
}

public struct EntityUpdate()
{
    public List<ComponentBox> AddedComponents { get; set; } = [];
    public List<ComponentBox> RemovedComponents { get; set; } = [];
    public EntityUpdateKind Kind { get; set; } = EntityUpdateKind.Spawn;
}