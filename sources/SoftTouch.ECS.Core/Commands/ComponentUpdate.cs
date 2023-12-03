using Microsoft.Extensions.ObjectPool;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public abstract class ComponentBox;

public class ComponentBox<TComp>() : ComponentBox
    where TComp : struct
{
    static ObjectPool<ComponentBox<TComp>> pool = ObjectPool.Create<ComponentBox<TComp>>();
    public static ComponentBox<TComp> Create(TComp? component = null)
        => pool.Get()
    
    public TComp Value { get; set; }

}

public class ComponentUpdates
{
    public GenerationalEntity Entity { get; set; }
    List<ComponentBox> Removals = [];
    List<ComponentBox> Adds = [];
    public void Update(WorldCommands commands)
    {

    }
}
