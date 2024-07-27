namespace SoftTouch.ECS.Components;


public struct Parent(in Storage.Entity entity)
{
    public Storage.Entity Entity { get; set; } = entity;
    public static implicit operator Parent(Storage.Entity entity) => new(entity);
}
public readonly struct Children(List<Storage.Entity> entities)
{
    public List<Storage.Entity> Entities { get; } = entities;

    public static implicit operator Children(List<Storage.Entity> children) => new(children);
}