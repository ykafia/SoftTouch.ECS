using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS.Querying;

public readonly ref struct FilteredQueryEntity<Q> where Q : struct, IFilteredEntityQuery
{
    readonly Archetype archetype;
    readonly int archetypeIndex;
    readonly Q query;

    public FilteredQueryEntity(Archetype archetype, int archetypeIndex, Q query)
    {
        this.archetype = archetype;
        this.archetypeIndex = archetypeIndex;
        this.query = query;
    }

    public readonly EntityMeta EntityIndex => query.World[archetype.EntityLookup[archetypeIndex]];

    public ref T Get<T>()
        where T : struct
    {
        if(!query.HasAccessTo<T>())
            throw new ArgumentException($"Cannot read from type {typeof(T).Name}");
        return ref archetype.GetComponentArray<T>().Span[archetype.EntityLookup[archetypeIndex]];
    }
    public void Set<T>(T value)
        where T : struct
    {
        if (!query.HasAccessTo<T>())
            throw new ArgumentException($"Cannot read from type {typeof(T).Name}");
        archetype.SetComponent(archetype.EntityLookup[archetypeIndex], value);
    }

    public void Add<T>(in T c) where T : struct
    {
        if(query.HasAccessTo<T>())
            throw new Exception($"Cannot add component of type {typeof(T).Name}, component was already added");
        var idx = archetype.EntityLookup[archetypeIndex];
        var meta = archetype.World.Entities[idx];
        query.World.Commands.AddComponent(
            new(idx,meta.Entity.Generation), 
            in c
        );
    }
    public void Remove<T>() where T : struct
    {
        if (!query.HasAccessTo<T>())
            throw new Exception($"Cannot remove component of type {typeof(T).Name}, component does not exist");
        var idx = archetype.EntityLookup[archetypeIndex];
        var meta = archetype.World.Entities[idx];
        query.World.Commands.RemoveComponent<T>(
            meta.Entity
        );
    }
    public void Despawn()
    {
        var idx = archetype.EntityLookup[archetypeIndex];
        var meta = archetype.World.Entities[idx];
        query.World.Commands.Updates.Add(new DespawnEntity(new(idx, meta.Entity.Generation)));
    }
}
