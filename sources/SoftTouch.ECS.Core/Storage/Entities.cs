namespace SoftTouch.ECS.Storage;




/// <summary>
/// A storage for entity metadata which also serves as an entity allocator.
/// Entities are lazily spawned, Ids are first reserved, the reserved EntityId is added to PendingIds.
/// When an entity is freed, the ID is added to the FreeIds.
/// When an entity is finally spawned, PendingIds is emptied.
/// </summary>
public struct Entities()
{
    public List<EntityMeta> Meta { get; set; } = [];
    public List<int> PendingIds { get; set; } = [];
    public List<int> FreeIds { get; set; } = [];
}