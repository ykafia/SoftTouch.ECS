namespace SoftTouch.ECS.Storage;




/// <summary>
/// A storage for entity metadata which also serves as an entity allocator.
/// Entities are lazily spawned, Ids are first reserved, the reserved EntityId is added to PendingIds.
/// When an entity is freed, the ID is added to the FreeIds.
/// When an entity is finally spawned, PendingIds is emptied.
/// </summary>
public class Entities(World world)
{
    World world = world;
    public List<EntityMeta> Meta { get; set; } = [];
    public List<int> ReservedIds { get; set; } = [];
    public List<int> FreeIds { get; set; } = [];

    public EntityMeta this[int index] => Meta[index];


    public void SetFree(in Entity entity)
    {
        FreeIds.Add(entity.Index);
    }
    public Entity GetOrCreate()
    {
        if (FreeIds.Count > 0)
        {
            // If there are free indices we use this free index but have to increment the generation.
            var id = FreeIds[0];
            FreeIds.RemoveAt(0);
            Meta[id] = Meta[id] with { Generation = Meta[id].Generation + 1 };
            ReservedIds.Add(id);
            return new(id, Meta[id].Generation);
        }
        else
        {
            // if there are no free indices that means we have to create a new one of generation 0
            var id = Meta.Count;
            ReservedIds.Add(id);
            return new(id, 0);
        }
    }
}