using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public sealed record SpawnEntity(Entity Entity) : EntityUpdate(Entity);
public sealed record DespawnEntity(Entity Entity) : EntityUpdate(Entity);
public sealed record ArchUpdate(Entity Entity) : EntityUpdate(Entity);

public abstract record EntityUpdate(Entity Entity) : IDisposable
{
    public ReusableList<Type> OperationTypes { get; } = [];
    public ReusableList<ComponentUpdate> ComponentUpdates { get; } = [];

    public virtual void Add<T>(in T component) where T : struct
    {
        var index = 0;
        foreach(var t in OperationTypes)
        {
            if(t == typeof(T) && ComponentUpdates[index].Operation == ComponentOperation.Add)
            {
                ((ComponentBox<T>)ComponentUpdates[index].Component).Value = component;
                return;
            }
            else if(t == typeof(T) && ComponentUpdates[index].Operation == ComponentOperation.Remove)
            {
                ComponentUpdates.Remove(ComponentUpdates[index]);
                OperationTypes.Remove(t);
                return;
            }
            index += 1;
        }
        OperationTypes.Add(typeof(T));
        ComponentUpdates.Add(new(ComponentBox<T>.Create(component), ComponentOperation.Add));
    }

    public void Remove<T>() where T : struct
    {
        var index = 0;
        foreach(var t in OperationTypes)
        {
            if(t == typeof(T) && ComponentUpdates[index].Operation == ComponentOperation.Remove)
                return;
            else if(t == typeof(T) && ComponentUpdates[index].Operation == ComponentOperation.Add)
            {
                ComponentUpdates.Remove(ComponentUpdates[index]);
                OperationTypes.Remove(t);
                return;
            }
            index += 1;
        }
        OperationTypes.Add(typeof(T));
        ComponentUpdates.Add(new(ComponentBox<T>.Create(), ComponentOperation.Add));
    }
    public void Dispose()
    {
        foreach (var c in ComponentUpdates.Span)
            c.Component.Dispose();
        ComponentUpdates.Dispose();
    }
}