using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;


public ref struct QueryEntity<Q>(Archetype archetype, int archetypeIndex, Q query)
    where Q : struct, IEntityQuery
{
    Archetype archetype = archetype;
    int archetypeIndex = archetypeIndex;
    Q query = query;

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
            new(idx,meta.Generation), 
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
            new(idx, meta.Generation)
        );
    }

    public void Despawn()
    {
        var idx = archetype.EntityLookup[archetypeIndex];
        var meta = archetype.World.Entities[idx];
        query.World.Commands.Updates.Add(new DespawnEntity(new(idx, meta.Generation)));
    }
}



public ref struct WorldQueryEnumerator<Q>
    where Q : struct, IEntityQuery
{
    Q query;
    World world => query.World;
    ArchetypeList.Enumerator enumerator;
    Archetype currentArchetype => enumerator.Current.Value;

    bool inArch;
    int archIndex;


    public QueryEntity<Q> Current => new(currentArchetype, archIndex, query);

    public WorldQueryEnumerator(Q  query)
    {
        this.query = query;
        enumerator = world.Archetypes.GetEnumerator();
        inArch = false;
        archIndex = 0;
    }


    public bool MoveNext()
    {
        if(inArch)
        {
            if (archIndex + 1 < currentArchetype.Length)
            {
                archIndex += 1;
                return true;
            }
            else
            {
                inArch = false;
                archIndex = 0;
            }
        }

        while (enumerator.MoveNext())
        {
            if (MatchArch(enumerator.Current.Key))
            {
                inArch = true;
                return true;
            }
        }
        
        return false;
    }

    public readonly bool MatchArch(ArchetypeID id)
    {
        if (id.Types == null || id.Types?.Length == 0)
            return false;
        else
        {
            foreach(var t in Q.Types)
                if(id.Types == null || id.Types != null && !id.Types.Contains(t))
                    return false;
            return true;
        }
    }
}