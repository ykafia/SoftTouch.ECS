using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public class ComponentAdd<T> : ComponentUpdate where T : struct
{
    public T Value {get;set;}

    public ComponentAdd(in T val, in Entity e)
    {
        Value = val;
        Entity = e;
    }

    public override void Update(WorldCommands commands)
    {
        Entity.AddComponent(Value);
    }
}
