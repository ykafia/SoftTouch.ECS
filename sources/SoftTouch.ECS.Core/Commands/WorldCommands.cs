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
            if(update.Kind == EntityUpdateKind.ComponentUpdate)
            {
                var entity = world.GEntities[update.Entity];

                var id = entity.Location.Archetype.ID;
            }
        }
    }
}