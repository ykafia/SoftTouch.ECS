using Microsoft.Extensions.ObjectPool;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public abstract class ComponentBox;

public class ComponentBox<TComp>() : ComponentBox
    where TComp : struct
{
    static ObjectPool<ComponentBox<TComp>> pool = ObjectPool.Create<ComponentBox<TComp>>();
    public static ComponentBox<TComp> Create(in TComp? component = null)
    {
        var result = pool.Get();
        if (component != null)
            result.Value = component.Value;
        return result;
    }

    public TComp Value { get; set; }

}

public class ComponentUpdates : IResettable
{
    static ObjectPool<ComponentUpdates> pool = ObjectPool.Create<ComponentUpdates>();

    public static ComponentUpdates Get() => pool.Get();

    public GenerationalEntity Entity { get; set; }
    public List<ComponentBox> Removals { get; } = [];
    public List<ComponentBox> Adds { get; } = [];

    public void Return() => pool.Return(this);

    public bool TryReset()
    {
        Entity = new();
        Removals.Clear();
        Adds.Clear();
        return true;
    }
}
