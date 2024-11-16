using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public sealed partial class World
{
    public AppTime? AppTime { get; }
    public WorldResources Resources { get; } = new();
    internal Entities Entities { get; }

    internal ArchetypeList Archetypes = new();

    public WorldCommands Commands => Resources.Get<WorldCommands>();

    public EntityMeta this[int id] => Entities[id];

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