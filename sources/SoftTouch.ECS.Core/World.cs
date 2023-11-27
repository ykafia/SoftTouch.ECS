using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public sealed partial class World
{
    public AppTime? AppTime { get; }
    public WorldResources Resources { get; } = new();
    internal List<Entity> Entities { get; } = [];

    internal ArchetypeList Archetypes = new();

    public WorldCommands Commands => Resources.Get<WorldCommands>();

    public Entity this[int id]
    {
        get => Entities[id];
    }

    public World()
    {
        Archetypes.Add(new(), Archetype.CreateEmpty(this));
        Resources.Set(new WorldCommands(this));
    }
    public World(AppTime appTime)
    {
        AppTime = appTime;
        Resources.Set(AppTime);
        Archetypes.Add(new(), Archetype.CreateEmpty(this));
        Resources.Set(new WorldCommands(this));
    }

    internal Archetype GenerateArchetype(ArchetypeID types, IEnumerable<ComponentArray> components)
    {
        if (!Archetypes.ContainsKey(types))
        {
            Archetypes.Add(types, new Archetype(components, this));
            return Archetypes[types];
        }
        else
            return Archetypes[types];
    }
    internal Archetype GenerateArchetype(ArchetypeID types, List<ComponentBase> components)
    {
        if (!Archetypes.ContainsKey(types))
        {
            Archetypes.Add(types, new Archetype(components, this));
            return Archetypes[types];
        }
        else
            return Archetypes[types];
    }

    public void BuildGraph()
    {
        var stor = Archetypes.Values;
        foreach (var arch in Archetypes.Values)
        {
            foreach (var x in stor)
            {
                if (x.ID.IsAddedType(arch.ID))
                {
                    arch.TypeExcept(x, out var types);
                    arch.Edges.Add[types[0]] = x;
                }

            }
            foreach (var x in stor)
            {
                if (x.ID.IsAddedType(arch.ID))
                {
                    arch.TypeExcept(x, out var types);
                    arch.Edges.Add[types[0]] = x;
                }
            }
        }
    }

    public IEnumerable<Archetype> QueryArchetypes(ArchetypeID types)
    {
        for (int i = 0; i < Archetypes.Count; i++)
        {
            if (Archetypes.Values[i].ID.IsSupersetOf(types.Span))
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
        BuildGraph();
    }
}
