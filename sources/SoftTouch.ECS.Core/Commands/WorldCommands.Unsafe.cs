using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands
{
    public void InsertOrSpawn<T>(Entity entity, in T component)
        where T : struct
    {
        // if entity exists add component
        #error should finish this function
        if (entity.Index < world.Entities.Meta.Count && !world.Entities.FreeIds.Contains(entity))
        {
            var meta = world.Entities[entity];
            if (meta.Entity.Generation < entity.Generation)
                meta.Entity = meta.Entity with { Generation = entity.Generation };
            if (meta.Entity == entity && meta.Location.Archetype.Has<T>())
                meta.Location.Archetype.SetComponent(meta.Location.RowIndex, component);
        }

        // else if there is an entity with a different generation return null

        // else spawn new one
    }
}