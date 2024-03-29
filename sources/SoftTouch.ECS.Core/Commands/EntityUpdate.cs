using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public enum EntityUpdateKind
{
    ComponentUpdate,
    Spawn,
    Despawn
}

public record class EntityUpdate(EntityUpdateKind Kind, Entity Entity) : IDisposable
{
    public ReusableList<ComponentBox> AddedComponents { get; } = new();
    public ReusableList<ComponentBox> RemovedComponents { get; } = new();

    public void Add<T>(in T component) where T : struct
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