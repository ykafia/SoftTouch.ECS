using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;

public struct EntityLookup
{
    public Dictionary<EntityId, int> ArchetypeIndices = [];
    public Dictionary<int, EntityId> EntityIndices = [];

    public readonly int Count => ArchetypeIndices.Count;


    public EntityLookup(Dictionary<EntityId,int> indices)
    {
        ArchetypeIndices = indices;
        EntityIndices = ArchetypeIndices.ToDictionary(x => x.Value, x => x.Key);
    }

    public readonly EntityId this[int e] => EntityIndices[e];

    public readonly int this[in EntityId e]
    {
        get { return ArchetypeIndices[e]; }
        set { ArchetypeIndices[e] = value; EntityIndices[value] = e; }
    }

    public readonly void Add(in EntityId e)
    {
        ArchetypeIndices.Add(e, ArchetypeIndices.Count);
        EntityIndices.Add(ArchetypeIndices.Count, e);
    }
    public readonly void Remove(in EntityId e)
    {
        EntityIndices.Remove(ArchetypeIndices.Count - 1);
        ArchetypeIndices.Remove(e);
    }

    public readonly bool TryGetValue(in EntityId id, out int index)
    {
        return ArchetypeIndices.TryGetValue(id, out index);
    }
    public readonly EntityId LookUp(int archId)
    {
        return EntityIndices[archId];
    }
    public readonly void Set(in EntityId id, int value)
    {
        ArchetypeIndices[id] = value;
        EntityIndices.Remove(value);
        EntityIndices[value] = id;
    }

    public readonly void Clear()
    {
        ArchetypeIndices.Clear();
        EntityIndices.Clear();
    }

    public readonly EntityLookup Clone()
    {
        return new()
        {
            EntityIndices = new(EntityIndices),
            ArchetypeIndices = new(ArchetypeIndices)
        };
    }
}
