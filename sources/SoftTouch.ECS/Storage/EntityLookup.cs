using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;

public class EntityLookup
{
    public Dictionary<EntityId, int> ArchetypeIndices = new();
    public Dictionary<int, EntityId> EntityIndices = new();

    public int Count => ArchetypeIndices.Count;

    public EntityLookup() { }

    public EntityLookup(Dictionary<EntityId,int> indices)
    {
        ArchetypeIndices = indices;
        EntityIndices = ArchetypeIndices.ToDictionary(x => x.Value, x => x.Key);
    }

    public EntityId this[int e]
    {
        get { return EntityIndices[e]; }
    }
    public int this[in EntityId e]
    {
        get { return ArchetypeIndices[e]; }
        set { ArchetypeIndices[e] = value; EntityIndices[value] = e; }
    }

    public void Add(in EntityId e)
    {
        ArchetypeIndices.Add(e, ArchetypeIndices.Count);
        EntityIndices.Add(ArchetypeIndices.Count, e);
    }
    public void Remove(in EntityId e)
    {
        EntityIndices.Remove(ArchetypeIndices.Count - 1);
        ArchetypeIndices.Remove(e);
    }

    public bool TryGetValue(in EntityId id, out int index)
    {
        return ArchetypeIndices.TryGetValue(id, out index);
    }
    public int LookUp(int archId)
    {
        return EntityIndices[archId];
    }
    public void Set(in EntityId id, int value)
    {
        ArchetypeIndices[id] = value;
        EntityIndices.Remove(value);
        EntityIndices[value] = id;
    }

    public int GetEntityId(int eIdx)
    {
        return EntityIndices[eIdx];
    }
}
