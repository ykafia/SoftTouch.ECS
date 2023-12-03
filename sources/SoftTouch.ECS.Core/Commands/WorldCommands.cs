using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public record struct EntityUpdates(GenerationalEntity Entity, List<ComponentUpdate> Updates, bool IsSpawn = false);

public partial class WorldCommands(World world)
{
    readonly World world = world;
    readonly List<EntityUpdates> updates = [];

    public void Add(ComponentUpdate update, bool isSpawn = false)
    {
        foreach(var e in updates)
            if(e.Entity == update.Entity)
            {
                e.Updates.Add(update);
                return;
            }
        updates.Add(new(update.Entity, [update], isSpawn));
    }


    public void ExecuteUpdates()
    {
        while (updates.TryDequeue(out var e))
        {
            e.Update(this);
        }
    }
}