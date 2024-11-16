using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS.Querying;

public readonly ref struct QueryEntity<Q>(Archetype archetype, int archetypeIndex, Q query)
    where Q : struct, IEntityQuery
{
    readonly Archetype archetype = archetype;
    readonly int archetypeIndex = archetypeIndex;
    readonly Q query = query;

    public readonly EntityMeta EntityIndex => query.World[archetype.EntityLookup[archetypeIndex]];
    public readonly Entity Entity => EntityIndex;

    public ref T Get<T>()
        where T : struct
    {
        if (!query.HasAccessTo<T>())
            throw new ArgumentException($"Cannot read from type {typeof(T).Name}");
        return ref archetype.GetComponentArray<T>().Span[archetypeIndex];
    }
    public void Set<T>(in T value)
        where T : struct
    {
        if (!query.HasAccessTo<T>())
            throw new ArgumentException($"Cannot read from type {typeof(T).Name}");
        archetype.SetComponent(archetypeIndex,value);
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
            new(idx, meta.Entity.Generation)
        );
    }

    public void Despawn()
    {
        var idx = archetype.EntityLookup[archetypeIndex];
        var meta = archetype.World.Entities[idx];
        query.World.Commands.Updates.Add(new DespawnEntity(new(idx, meta.Entity.Generation)));
    }
}
