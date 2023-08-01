using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public class ComponentAdd<T> : ComponentUpdate where T : struct, IEquatable<T>
{
    public T Value {get;set;}

    public ComponentAdd(in T val, in Entity e)
    {
        Value = val;
        EntityRecord = e;
    }

    public override void UpdateRecord()
    {
        EntityRecord.AddComponent(Value);
    }
}
