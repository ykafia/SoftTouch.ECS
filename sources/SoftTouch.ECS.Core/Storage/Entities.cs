using System.Security.Cryptography.X509Certificates;

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
    internal void Clear()
    {
        FreeIds.Clear();
        for(int i = 0; i < Meta.Count; i += 1)
        {
            FreeIds.Add(i);
            Meta[i] = Meta[i] with {Location = new EntityLocation(null!, 0)};
        }
    }
    public Entity GetOrCreate()
    {
        if (FreeIds.Count > 0)
        {
            // If there are free indices we use this free index but have to increment the generation.
            var id = FreeIds[0];
            FreeIds.RemoveAt(0);
            Meta[id] = Meta[id] with { Entity = new() { Index = id, Generation = Meta[id].Entity.Generation + 1 } };
            ReservedIds.Add(id);
            return Meta[id].Entity;
        }
        else
        {
            // if there are no free indices that means we have to create a new one of generation 0
            var id = Math.Max(Meta.Count, ReservedIds.Count > 0 ? ReservedIds.Max() + 1 : 0);
            ReservedIds.Add(id);
            return new(id, 0);
        }
    }
}