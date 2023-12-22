using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public partial class WorldCommands(World world)
{
    readonly World world = world;
    internal List<EntityUpdate> Updates { get; } = [];


    public void AddComponent<T>(in Entity e, in T component) where T : struct
    {
        foreach (var u in Updates)
        {
            if (u.Entity == e)
            {
                u.AddedComponents.Add(ComponentBox<T>.Create(component));
                return;
            }
        }
        var update = new EntityUpdate(EntityUpdateKind.ComponentUpdate, e);
        update.AddedComponents.Add(ComponentBox<T>.Create(component));
    }
    public void RemoveComponent<T>(in Entity e) where T : struct
    {
        foreach (var u in Updates)
        {
            if (u.Entity == e)
            {
                u.RemovedComponents.Add(ComponentBox<T>.Create(default));
                return;
            }
        }
        var update = new EntityUpdate(EntityUpdateKind.ComponentUpdate, e);
        update.RemovedComponents.Add(ComponentBox<T>.Create(default));
    }

    public void ExecuteUpdates()
    {
        foreach (var update in Updates)
        {
            if (update.Kind == EntityUpdateKind.Spawn)
            {
                // Create the archetype id
                var idArr = new Type[update.AddedComponents.Length];
                for (int i = 0; i < update.AddedComponents.Length; i++)
                    idArr[i] = update.AddedComponents.Span[i].ComponentType;
                var aid = new ArchetypeID(idArr);

                // Does our world have the archetype we need?

                if (world.Archetypes.TryGetValue(aid, out var arch))
                {
                    arch.AddEntity(update.Entity);
                    foreach (var cb in update.AddedComponents.Span)
                        arch.Storage[cb.ComponentType].TryAdd(cb);
                }
                else
                {
                    world.Archetypes.Add(aid, new Archetype(aid, update.AddedComponents, world));
                }

                // Add the meta data

                world.Entities.ReservedIds.Remove(update.Entity);
                // If the entity is already existing, increment the generation
                if (world.Entities.Meta.Count > update.Entity)
                    world.Entities.Meta[update.Entity] = world.Entities.Meta[update.Entity] with { Generation = update.Entity.Generation };
                else if(update.Entity.Index == world.Entities.Meta.Count)
                    world.Entities.Meta.Add(new(){Generation = update.Entity.Generation});
                else throw new NotImplementedException("Entity index skipped");

            }
            else if (update.Kind == EntityUpdateKind.Despawn)
            {
                // Remove all components
                world.Entities[update.Entity].Location.Archetype.RemoveEntity(update.Entity);
                // Put the entity to FreeIds                
                world.Entities.SetFree(update.Entity);
            }
            else if (update.Kind == EntityUpdateKind.ComponentUpdate)
            {
                throw new NotImplementedException();
            }
            update.Dispose();
        }
        Updates.Clear();
    }
}