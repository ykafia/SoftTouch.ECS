using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;


public ref struct QueryEntity<Q>
    where Q : struct, IEntityQuery
{
    Archetype archetype;
    int archetypeIndex;
    Q query;

    public QueryEntity(Archetype archetype, int archetypeIndex, Q query)
    {
        this.archetype = archetype;
        this.archetypeIndex = archetypeIndex;
        this.query = query;
    }

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
        throw new NotImplementedException();
        //query.World.AddArchetypeUpdate(new ComponentAdd<T>(c, new(archetype.EntityLookup.LookUp(archetypeIndex), archetype)));
    }
    public void Remove<T>() where T : struct
    {
        throw new NotImplementedException();
        //query.World.AddArchetypeUpdate(new ComponentRemove<T>(new(archetype.EntityLookup.LookUp(archetypeIndex), archetype)));
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
            if (MatchArch(enumerator.Current.Key))
                return true;
        
        return false;
    }

    public bool MatchArch(ArchetypeID id)
    {
        if (id.Types == null && id.Types?.Length == 0)
            return false;
        else
        {
            foreach(var t in Q.Types)
                if(!id.Types.Contains(t))
                    return false;
            return true;
        }
    }
}