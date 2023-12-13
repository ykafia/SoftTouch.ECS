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
                
            }
        }
    }
}