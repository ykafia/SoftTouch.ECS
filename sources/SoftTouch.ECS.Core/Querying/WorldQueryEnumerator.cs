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

    public T Get<T>()
        where T : struct, IEquatable<T>
    {
        // TODO : check if value is read

        archetype.GetComponent<T>(archetypeIndex, out var result);
        return result;
    }
    public void Set<T>(in T value)
        where T : struct, IEquatable<T>
    {
        // TODO : check if value is written
        archetype.SetComponent(archetypeIndex,value);
    }

    public void Add<T>(in T c) where T : struct, IEquatable<T>
    {
        query.World.AddArchetypeUpdate(new ComponentAdd<T>(c, new(archetype.EntityLookup.LookUp(archetypeIndex), archetype)));
    }
    public void Remove<T>() where T : struct, IEquatable<T>
    {
        query.World.AddArchetypeUpdate(new ComponentRemove<T>(new(archetype.EntityLookup.LookUp(archetypeIndex), archetype)));
    }
}



public ref struct WorldQueryEnumerator<Q>
    where Q : struct, IEntityQuery
{
    Q query;
    World world => query.World;
    IEnumerator<KeyValuePair<ArchetypeID, Archetype>> enumerator;
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
        if (id.Types == null)
            return false;
        if(Q.Read != null)
            foreach (var t in query.ImplRead)
                if (!id.Types.Contains(t))
                    return false;
        if(Q.Write != null)
            foreach (var t in query.ImplWrite)
                if (!id.Types.Contains(t))
                    return false;
        return true;
    }
}