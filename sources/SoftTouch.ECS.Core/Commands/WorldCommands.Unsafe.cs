using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands
{
    public void InsertOrSpawn<T>(Entity entity, RefArchetypeID aid, ReadOnlySpan<ComponentBox> components)
        where T : struct
    {
        // If archetype exists, insert entity
        if (world.Archetypes.Has(aid))
        {
            var meta = world.Entities[entity];
            if (meta.Entity.Generation < entity.Generation)
                meta.Entity = meta.Entity with { Generation = entity.Generation };
            foreach(var component in components)
                if (meta.Entity == entity && meta.Location.Archetype.Has<T>())
                    meta.Location.Archetype.SetComponent(meta.Location.RowIndex, component);
        }
        // If archetype does not exist, create archetype and add entity
        else
        {
            var archetype = new Archetype(world, entity, components);
            world.Archetypes.Add([typeof(T)], archetype);
        }
        foreach(var comp in components)
            comp.Dispose();
    }
}