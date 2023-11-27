namespace SoftTouch.ECS.Storage;


public record struct GenerationalEntity(int Index, int Generation);

public record struct EntityMeta(int Generation, EntityLocation Location);

public record struct EntityLocation(Archetype Archetype, int RowIndex);