using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public partial class WorldCommands(World world)
{
    readonly World world = world;
    readonly List<ComponentUpdates> updates = [];

    public void ExecuteUpdates()
    {
        throw new NotImplementedException();
    }
}