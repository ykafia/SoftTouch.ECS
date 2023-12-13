using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public partial class WorldCommands(World world)
{
    readonly World world = world;
    readonly List<EntityUpdate> updates = [];

    public void ExecuteUpdates()
    {
        foreach(var update in updates)
        {
            if(update.Kind == EntityUpdateKind.Spawn)
            {
                // world.GEntities[update.Entity].Generation =
                var idArr = new Type[update.AddedComponents.Length];
                for(int i = 0; i < update.AddedComponents.Length; i++)
                    idArr[i] = update.AddedComponents.Span[i].ComponentType;
                var aid = new ArchetypeID(idArr);

                // Does our world have the archetype we need?

                if(world.Archetypes.TryGetValue(aid,out var arch))
                {
                    arch.AddEntity(update.Entity);
                    foreach(var cb in update.AddedComponents.Span)
                        arch.Storage[cb.ComponentType].TryAdd(cb);
                }
                else
                {
                    world.Archetypes.Add(aid, new Archetype(aid, update.AddedComponents, world));
                }
            }
            else if(update.Kind == EntityUpdateKind.Despawn)
            {
                // Remove all components
                world.GEntities[update.Entity].Location.Archetype.RemoveEntity(update.Entity);
                // Put the entity to FreeIds                
                world.GEntities.SetFree(update.Entity);
            }
            else if (update.Kind == EntityUpdateKind.ComponentUpdate)
            {
                throw new NotImplementedException();
            }
            update.Dispose();
        }
    }
}