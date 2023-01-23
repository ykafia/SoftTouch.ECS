using SoftTouch.ECS.Arrays;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SoftTouch.ECS.Storage;

public struct Entity
{
    public long Index { get; init; }

    public Archetype Archetype;

    public World World => Archetype.World;

    public int ArchetypeIndex => Archetype.EntityLookup[Index];

    public Entity(long index, Archetype archetype)
    {
        Index = index;
        Archetype = archetype;
    }



    public void Set<T>(T cmp) where T : struct
        => Archetype.SetComponent(ArchetypeIndex, cmp);

    public T Get<T>() where T : struct
        => Archetype.GetComponentArray<T>()[ArchetypeIndex];

    public bool Has<T>()
        => Archetype.Storage.ContainsKey(typeof(T));

    public void Add<T>(in T c) where T : struct
    {
        World.AddArchetypeUpdate(new ComponentAdd<T>(c, this));
    }
    public void Remove<T>() where T : struct
    {
        World.AddArchetypeUpdate(new ComponentRemove<T>(this));
    }

    internal void AddComponent<T>(in T c) where T : struct
    {
        var arrays = Archetype.Storage;

        if (Archetype.Edges.Add.TryGetValue(typeof(T), out var newArch))
        {
            // Add all components to new archetype
            foreach (var cmp in arrays)
            {
                cmp.Value.TransferTo(newArch.Storage[cmp.Key], ArchetypeIndex);
            }
            // Add entity
            newArch.SetComponent(c, Index);
            // Remove Entity from old
            Archetype.RemoveEntity(Index);
            // Change archetype
            Archetype = newArch;

        }
        else
        {
            var aid = new ArchetypeID(arrays.Keys.Append(typeof(T)).ToArray());

            var world = World;
            var arch = world.GenerateArchetype(aid, arrays.Values.Append(new ComponentList<T>()));
            arch.SetComponent(c, Index);

            foreach (var (type, cmps) in arrays)
                cmps.TransferTo(arch.Storage[type], ArchetypeIndex);

            Archetype.RemoveEntity(Index);
            Archetype = arch;
            world.BuildGraph();
        }
    }
    internal void RemoveComponent<T>() where T : struct
    {
        var arrays = Archetype.Storage;
        if (Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
        {
            // Add all components to new archetype
            foreach (var cmp in arrays.Where(x => x.Key != typeof(T)))
                cmp.Value.TransferTo(newArch.Storage[cmp.Key], ArchetypeIndex);
            // Add entity
            newArch.AddEntity(Index);
            // Remove Entity from old
            Archetype.RemoveEntity(Index);
            // Change archetype
            Archetype = newArch;
        }
        else
        {
            var aid = new ArchetypeID(arrays.Select(c => c.Key).Where(ty => ty != typeof(T)).ToArray());

            var world = World;
            var arch = world.GenerateArchetype(aid, arrays.Where(c => c.Key != typeof(T)).Select(x => x.Value));
            arch.AddEntity(Index);
            foreach (var cmp in arrays.Where(x => x.Key != typeof(T)))
                cmp.Value.TransferTo(arch.Storage[cmp.Key], ArchetypeIndex);
            Archetype.RemoveEntity(Index);
            Archetype = arch;
            world.BuildGraph();
        }
    }

    public override string ToString()
    {
        return Archetype.ToString();
    }

}