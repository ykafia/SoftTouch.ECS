using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands
{
    World world;
    Queue<ComponentUpdate> updates;

    public WorldCommands(World world)
    {
        this.world = world;
        updates = new();
    }

    public void Enqueue(ComponentUpdate update)
    {
        updates.Enqueue(update);
    }


    public void ExecuteUpdates()
    {
        while (updates.TryDequeue(out var e))
        {
            e.UpdateRecord();
        }
    }
}