using System.Collections.Concurrent;
using System.Dynamic;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public sealed partial class World
{
    public AppTime? AppTime { get; }
    public ConcurrentDictionary<Type, object> Resources { get; } = [];
    internal Entities Entities { get; }

    internal ArchetypeList Archetypes = new();

    public WorldCommands Commands => Resources.Get<WorldCommands>();

    public EntityMeta this[int id] => Entities[id];
    public int Count => Entities.Meta.Count;

    public World()
    {
        Archetypes.Add(new(), Archetype.CreateEmpty(this));
        Resources.Set(new WorldCommands(this));
        Entities = new(this);
    }
    public World(AppTime appTime)
    {
        AppTime = appTime;
        Resources.Set(AppTime);
        Resources.Set(new EventsResource());
        Archetypes.Add(new(), Archetype.CreateEmpty(this));
        Resources.Set(new WorldCommands(this));
        Entities = new(this);
    }
    public TResource GetResource<TResource>() where TResource : class
        => Resources.Get<TResource>();

    public void Clear()
    {
        foreach (var (_, arch) in Archetypes)
            arch.Clear();
        Entities.Clear();
    }
}

public static class ResourceExtensions
{
    public static T Get<T>(this ConcurrentDictionary<Type, object> resource)
        where T : class
        => (T)resource[typeof(T)];
    public static void Set<T>(this ConcurrentDictionary<Type, object> resource, T value)
        where T : class
        => resource[typeof(T)] = value;
}
