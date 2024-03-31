using CommunityToolkit.HighPerformance.Buffers;
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
                // Find the archetype
                Archetype arch = null!;
                foreach (var a in world.Archetypes.Values)
                {
                    arch = a;
                    foreach (var t in update.AddedComponents.Span)
                        if (!a.ID.Contains(t.ComponentType))
                        {
                            arch = null!;
                            break;
                        }
                    if(arch != null)
                        break;
                }
                if (arch is null)
                {
                    var idArr = new Type[update.AddedComponents.Length];
                    for (int i = 0; i < update.AddedComponents.Length; i++)
                        idArr[i] = update.AddedComponents.Span[i].ComponentType;
                    var aid = new ArchetypeID(idArr);
                    world.Archetypes.Add(aid, new Archetype(aid, update.AddedComponents, world));
                    arch = world.Archetypes[aid];
                    var meta = new EntityMeta() { Generation = update.Entity.Generation, Location = new(arch, 0) };
                    world.Entities.Meta.Add(meta);
                    arch.AddEntity(update.Entity.Index);
                }
                else
                {
                    // Does our world have the archetype we need?

                    // Add the meta data

                    // First Step remove the reserved ids since we're spawing the entity again
                    world.Entities.ReservedIds.Remove(update.Entity);

                    // If the size of the entities is 0, Add the meta information
                    if (world.Entities.Meta.Count == 0)
                    {
                        var meta = new EntityMeta() { Generation = update.Entity.Generation, Location = new(arch, 0) };
                        world.Entities.Meta.Add(meta);
                        arch.AddEntity(update.Entity.Index);
                        foreach(var comp in update.AddedComponents.Span)
                            arch.Storage[comp.ComponentType].TryAdd(comp);
                    }
                    // Else check if the entity id already exists and increment its generation
                    else if (update.Entity.Index < world.Entities.Meta.Count)
                        world.Entities.Meta[update.Entity] = world.Entities.Meta[update.Entity] with { Generation = update.Entity.Generation };
                    // Else add the entity
                    else if (world.Entities.Meta.Count == update.Entity.Index)
                    {
                        var meta = new EntityMeta() { Generation = update.Entity.Generation, Location = new(arch, arch.Length) };
                        world.Entities.Meta.Add(meta);
                        arch.AddEntity(update.Entity.Index);
                        foreach (var comp in update.AddedComponents.Span)
                            arch.Storage[comp.ComponentType].TryAdd(comp);
                    }
                    else throw new Exception("Entity index were skipped");
                }
                

            }
            else if (update.Kind == EntityUpdateKind.Despawn)
            {
                // Remove all components
                var location = world.Entities[update.Entity].Location;
                location.Archetype.RemoveEntity(update.Entity);
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