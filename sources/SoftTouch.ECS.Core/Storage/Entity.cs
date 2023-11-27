using SoftTouch.ECS.Arrays;
namespace SoftTouch.ECS.Storage;

public struct Entity
{
    EntityId index;
    public EntityId Index => index;

    public Archetype Archetype { get; set; }

    public World World => Archetype.World;

    public int ArchetypeIndex
    {
        get
        {
            if (Archetype.EntityLookup.TryGetValue(in index, out var res))
                return res;
            return -1;
        }
    }

    public Entity(int index, Archetype archetype)
    {
        this.index = new(index);
        Archetype = archetype;
    }



    public bool Has<T>()
        => Archetype.Storage.ContainsKey(typeof(T));
    // public void Set<T>(in T cmp) where T : struct
    //         => Archetype.SetComponent(ArchetypeIndex, in cmp);

    // public T Get<T>() where T : struct
    //     => Archetype.GetComponentArray<T>()[ArchetypeIndex];


    // public void Add<T>(in T c) where T : struct
    // {
    //     World.AddArchetypeUpdate(new ComponentAdd<T>(c, in this));
    // }
    // public void Remove<T>() where T : struct
    // {
    //     World.AddArchetypeUpdate(new ComponentRemove<T>(in this));
    // }

    // internal void AddComponent<T>(in T c) where T : struct
    // {
    //     var arrays = Archetype.Storage;

    //     if (Archetype.Edges.Add.TryGetValue(typeof(T), out var newArch))
    //     {
    //         // Add all components to new archetype
    //         foreach (var cmp in arrays)
    //         {
    //             cmp.Value.MoveTo(ArchetypeIndex, newArch.Storage[cmp.Key]);
    //         }
    //         // Add entity
    //         newArch.SetEntityComponent(in index, c);
    //         // Remove Entity from old
    //         Archetype.RemoveEntity(Index);
    //         // Change archetype
    //         World.Entities[Index] = this with { Archetype = newArch };
    //     }
    //     else
    //     {
    //         var aid = new ArchetypeID(arrays.Keys.Append(typeof(T)).ToArray());

    //         var world = World;
    //         var arch = world.GenerateArchetype(aid, arrays.Values.Append(new ComponentArray<T>()));
    //         arch.SetEntityComponent(in index, c);

    //         foreach (var (type, cmps) in arrays)
    //             cmps.MoveTo(ArchetypeIndex, arch.Storage[type]);

    //         Archetype.RemoveEntity(Index);
    //         World.Entities[Index] = this with { Archetype = arch };
    //         world.BuildGraph();
    //     }
    // }
    // internal void RemoveComponent<T>() where T : struct
    // {
    //     var arrays = Archetype.Storage;
    //     if (Archetype.Edges.Remove.TryGetValue(typeof(T), out var newArch))
    //     {
    //         // Add all components to new archetype
    //         foreach (var cmp in arrays.Where(x => x.Key != typeof(T)))
    //             cmp.Value.MoveTo(ArchetypeIndex, newArch.Storage[cmp.Key]);
    //         // Add entity
    //         newArch.AddEntity(Index);
    //         // Remove Entity from old
    //         Archetype.RemoveEntity(Index);
    //         // Change archetype
    //         World.Entities[Index] = this with { Archetype = newArch };
    //     }
    //     else
    //     {
    //         var aid = new ArchetypeID(arrays.Select(c => c.Key).Where(ty => ty != typeof(T)).ToArray());

    //         var world = World;
    //         var arch = world.GenerateArchetype(aid, arrays.Where(c => c.Key != typeof(T)).Select(x => x.Value));
    //         arch.AddEntity(Index);
    //         foreach (var cmp in arrays.Where(x => x.Key != typeof(T)))
    //             cmp.Value.MoveTo(ArchetypeIndex, arch.Storage[cmp.Key]);
    //         Archetype.RemoveEntity(Index);
    //         World.Entities[Index] = this with { Archetype = arch };
    //         world.BuildGraph();
    //     }
    // }

    public override string ToString()
    {
        return Archetype.ToString();
    }

}