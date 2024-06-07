using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public sealed record SpawnEntity(Entity Entity) : EntityUpdate(Entity);
public sealed record DespawnEntity(Entity Entity) : EntityUpdate(Entity);
public sealed record ArchUpdate(Entity Entity) : EntityUpdate(Entity);

public abstract record EntityUpdate(Entity Entity) : IDisposable
{
    public ReusableList<ComponentBox> AddedComponents { get; } = [];
    public ReusableList<ComponentBox> RemovedComponents { get; } = [];

    public virtual void Add<T>(in T component) where T : struct
    {
        foreach(var e in AddedComponents.Span)
            if(e is ComponentBox<T>)
                return;
        AddedComponents.Add(ComponentBox<T>.Create(component));
    }

    public void Remove<T>(in T component) where T : struct
    {
        foreach (var e in RemovedComponents.Span)
            if (e is ComponentBox<T>)
                return;
        AddedComponents.Add(ComponentBox<T>.Create(component));
    }
    public void Dispose()
    {
        try
        {
            foreach (var c in AddedComponents.Span)
                c.Dispose();
        }catch (Exception) { }
        try
        {
            foreach (var c in RemovedComponents.Span)
                c.Dispose();
        }
        catch (Exception) { }

        AddedComponents.Dispose();
        RemovedComponents.Dispose();
    }
}