using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

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