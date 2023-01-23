using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public class ComponentRemove<T> : ComponentUpdate where T : struct
{
    public Type ToRemove => typeof(T);
    public ComponentRemove(in Entity e)
    {
        EntityRecord = e;
    }

    public override void UpdateRecord()
    {
        EntityRecord.RemoveComponent<T>();
    }
}
