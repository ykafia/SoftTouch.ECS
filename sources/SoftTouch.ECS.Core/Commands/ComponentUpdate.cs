using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public abstract class ComponentUpdate
{
    public Entity Entity { get; set; }
    public abstract void Update(WorldCommands commands);
}
