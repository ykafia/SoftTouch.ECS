using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public ref struct WorldQueryEnumerator<Q>
    where Q : struct, IFilteredEntityQuery
{
    Q query;
    World world => query.World;
    ArchetypeList.Enumerator enumerator;
    Archetype currentArchetype => enumerator.Current.Value;

    bool inArch;
    int archIndex;

    public QueryEntity<Q> Current => new(currentArchetype, archIndex, query);

    public WorldQueryEnumerator(Q query)
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
        if (id.Types == null || id.Types != null && id.Types?.Length == 0)
            return false;
        else if(id.Types != null)
        {
            foreach (var t in Q.Types)
                if (!id.Types.Contains(t))
                    return false;
            foreach(var t in Q.Filters.ImplWithTypes)
                if (!id.Types.Contains(t))
                    return false;
            foreach (var t in Q.Filters.ImplWithoutTypes)
                if (id.Types.Contains(t))
                    return false;
            return true;
        }
        return false;
    }
}
