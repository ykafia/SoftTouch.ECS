using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public class ResourceUpdate<T> : ComponentUpdate where T : struct, IEquatable<T>
{
    public T Value {get;set;}

    public ResourceUpdate(in T val, Entity e)
    {
        Value = val;
        EntityRecord = e;
    }

    public override void UpdateRecord()
    {
        EntityRecord.AddComponent(Value);
    }
}
