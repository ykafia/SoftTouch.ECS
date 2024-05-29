using SoftTouch.ECS.Arrays;
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
        Archetypes.Add(new(), Archetype.CreateEmpty(this));
        Resources.Set(new WorldCommands(this));
        Entities = new(this);
    }
    public TResource GetResource<TResource>() where TResource : class 
        => Resources.Get<TResource>();

    public IEnumerable<Archetype> QueryArchetypes(ArchetypeID types)
    {
        for (int i = 0; i < Archetypes.Count; i++)
        {
            if (Archetypes.Values[i].ID.IsSupersetOf(types))
                yield return Archetypes.Values[i];
        }
    }
    public IEnumerable<Archetype> Query<T>()
        where T : struct
    {
        for (int i = 0; i < Archetypes.Count; i++)
        {
            if (Archetypes.Values[i].ID.Types.Contains(typeof(T)))
                yield return Archetypes.Values[i];
        }
    }
    public IEnumerable<Archetype> Query<T1, T2>()
        where T1 : struct
        where T2 : struct
    {
        for (int i = 0; i < Archetypes.Count; i++)
        {
            if (
                Archetypes.Values[i].ID.Types.Contains(typeof(T1))
                && Archetypes.Values[i].ID.Types.Contains(typeof(T2))
                )
                yield return Archetypes.Values[i];
        }
    }
    public IEnumerable<Archetype> Query<T1, T2, T3>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        for (int i = 0; i < Archetypes.Count; i++)
        {
            if (
                Archetypes.Values[i].ID.Types.Contains(typeof(T1))
                && Archetypes.Values[i].ID.Types.Contains(typeof(T2))
                && Archetypes.Values[i].ID.Types.Contains(typeof(T3))
                )
                yield return Archetypes.Values[i];
        }
    }

    public void CopyArchetype(Archetype arch)
    {
        if (Archetypes.TryGetValue(arch.ID, out var existing))
        {
            existing.Clear();
            foreach (var t in arch.ID.Span)
            {
                arch.Storage[t].CopyTo(existing.Storage[t]);
            }
            existing.EntityLookup = arch.EntityLookup;
        }
        else
        {
            Archetypes[arch.ID] = arch.Clone();
        }
    }

}