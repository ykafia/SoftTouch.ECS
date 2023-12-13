using Microsoft.Extensions.ObjectPool;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public abstract class ComponentBox
{
    public abstract Type ComponentType { get; }
    public abstract ComponentArray ToArray();
    public abstract void Dispose();
}

public class ComponentBox<TComp>() : ComponentBox
    where TComp : struct
{
    static ObjectPool<ComponentBox<TComp>> pool = ObjectPool.Create<ComponentBox<TComp>>();

    public override Type ComponentType { get; } = typeof(TComp);

    public static ComponentBox<TComp> Create(in TComp? component = null)
    {
        var result = pool.Get();
        if (component != null)
            result.Value = component.Value;
        return result;
    }

    public TComp Value { get; set; }

    public override void Dispose()
    {
        pool.Return(this);
    }

    public override ComponentArray ToArray()
    {
        var result = new ComponentArray<TComp>();
        result.Add(Value);
        pool.Return(this);
        return result;
    }
}
