using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;


public ref struct FilteredQueryEntity<Q>
    where Q : struct, IFilteredEntityQuery
{
    Archetype archetype;
    int archetypeIndex;
    Q query;

    public FilteredQueryEntity(Archetype archetype, int archetypeIndex, Q query)
    {
        this.archetype = archetype;
        this.archetypeIndex = archetypeIndex;
        this.query = query;
    }

    public T Get<T>()
        where T : struct, IEquatable<T>
    {
        // TODO : check if value is read
        archetype.GetComponent<T>(archetype.EntityLookup[archetypeIndex], out var result);
        return result;
    }
    public void Set<T>(T value)
        where T : struct, IEquatable<T>
    {
        // TODO : check if value is written
        archetype.SetComponent(archetype.EntityLookup[archetypeIndex], value);
    }
}




public ref struct WorldFilteredQueryEnumerator<Q>
    where Q : struct, IFilteredEntityQuery
{
    Q query;
    World world => query.World;
    IEnumerator<KeyValuePair<ArchetypeID, Archetype>> enumerator;
    Archetype currentArchetype => enumerator.Current.Value;

    bool inArch;
    int archIndex;

    public QueryEntity<Q> Entity => new(currentArchetype, archIndex, query);

    public WorldFilteredQueryEnumerator(Q query)
    {
        this.query = query;
        enumerator = world.Archetypes.GetEnumerator();
        inArch = false;
        archIndex = 0;
    }


    public bool MoveNext()
    {
        if (inArch)
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
        if(Q.Read != null)
            foreach (var t in query.ImplRead)
                if (!id.Types.Contains(t))
                    return false;
        if(Q.Write != null)
            foreach (var t in query.ImplWrite)
                if (!id.Types.Contains(t))
                    return false;
        foreach (var t in Q.Filters.ImplWithTypes)
            if (!id.Types.Contains(t))
                return false;
        foreach (var t in Q.Filters.ImplWithoutTypes)
            if (id.Types.Contains(t))
                return false;

        return true;
    }
}
