using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands
{
    World world;
    Queue<ComponentUpdate> updates;
    private object locker = new();

    public WorldCommands(World world)
    {
        this.world = world;
        updates = new();
    }

    public void Enqueue(ComponentUpdate update)
    {
        lock(locker) 
            updates.Enqueue(update);
    }


    public void ExecuteUpdates()
    {
        while(updates.TryDequeue(out var e))
        {
            e.UpdateRecord();
        }
    }
}