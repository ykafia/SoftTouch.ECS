using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public class ComponentRemove<T> : ComponentUpdate where T : struct
{
    public Type ToRemove => typeof(T);
    public ComponentRemove(in Entity e)
    {
        Entity = e;
    }

    public override void Update(WorldCommands commands)
    {
        Entity.RemoveComponent<T>();
    }
}
