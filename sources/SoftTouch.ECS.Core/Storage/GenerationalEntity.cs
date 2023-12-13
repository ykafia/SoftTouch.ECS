namespace SoftTouch.ECS.Storage;


public record struct Entity(int Index, int Generation)
{
    public static implicit operator int(Entity entity) => entity.Index;
}

public record struct EntityMeta(int Generation, EntityLocation Location);

public record struct EntityLocation(Archetype Archetype, int RowIndex);