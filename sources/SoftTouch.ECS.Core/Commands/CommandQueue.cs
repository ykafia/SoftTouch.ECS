using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public partial class WorldCommands : Queue<ComponentUpdate>
{
    World world;

    public WorldCommands(World world)
    {
        this.world = world;
    }


    public void ExecuteUpdates()
    {
        while(TryDequeue(out var e))
        {
            e.UpdateRecord();
        }
    }
}