namespace SoftTouch.ECS.Storage;


public record struct Entity(int Index, int Generation)
{
    public static implicit operator int(Entity entity) => entity.Index;
    public static implicit operator Entity(EntityMeta entity) => entity.Entity;
}

public record struct EntityMeta(Entity Entity, EntityLocation Location);

public record struct EntityLocation(Archetype Archetype, int RowIndex);