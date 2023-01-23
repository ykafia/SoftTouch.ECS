using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

public ref struct QueryEnumerator<T1>
    where T1 : struct
{
    World world;
    List<Archetype> archetypes => (List<Archetype>)world.Archetypes.Values;
    int archIdx;
    int eIdx;

    public Entity Current => world[archetypes[archIdx][eIdx]];
    
    public QueryEnumerator(World world)
    {
        this.world = world;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !archetypes[archIdx].IsSupersetOf<T1>() || archetypes[archIdx].Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= archetypes[archIdx].Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (archetypes[archIdx].Length > 0 && archetypes[archIdx].IsSupersetOf<T1>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}
