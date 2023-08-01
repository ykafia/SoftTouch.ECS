using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{
    public Archetype() { }

    public static Archetype Create<T1>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1
        )
        where T1 : struct, IEquatable<T1>
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1)}
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })
            
        };
    }

    public static Archetype Create<T1, T2>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1)},
                { typeof(T2), new ComponentArray<T2>().With(component2)},
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }


    public static Archetype Create<T1, T2, T3>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2,
            in T3 component3
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1) },
                { typeof(T2), new ComponentArray<T2>().With(component2) },
                { typeof(T3), new ComponentArray<T3>().With(component3) },
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1) },
                { typeof(T2), new ComponentArray<T2>().With(component2) },
                { typeof(T3), new ComponentArray<T3>().With(component3) },
                { typeof(T4), new ComponentArray<T4>().With(component4) },
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1) },
                { typeof(T2), new ComponentArray<T2>().With(component2) },
                { typeof(T3), new ComponentArray<T3>().With(component3) },
                { typeof(T4), new ComponentArray<T4>().With(component4) },
                { typeof(T5), new ComponentArray<T5>().With(component5) },
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5, T6>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1) },
                { typeof(T2), new ComponentArray<T2>().With(component2) },
                { typeof(T3), new ComponentArray<T3>().With(component3) },
                { typeof(T4), new ComponentArray<T4>().With(component4) },
                { typeof(T5), new ComponentArray<T5>().With(component5) },
                { typeof(T6), new ComponentArray<T6>().With(component6) },
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5, T6, T7>(
            World world, 
            in ArchetypeID id, 
            int entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6,
            in T7 component7
        )
        where T1 : struct, IEquatable<T1>
        where T2 : struct, IEquatable<T2>
        where T3 : struct, IEquatable<T3>
        where T4 : struct, IEquatable<T4>
        where T5 : struct, IEquatable<T5>
        where T6 : struct, IEquatable<T6>
        where T7 : struct, IEquatable<T7>
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentArray<T1>().With(component1) },
                { typeof(T2), new ComponentArray<T2>().With(component2) },
                { typeof(T3), new ComponentArray<T3>().With(component3) },
                { typeof(T4), new ComponentArray<T4>().With(component4) },
                { typeof(T5), new ComponentArray<T5>().With(component5) },
                { typeof(T6), new ComponentArray<T6>().With(component6) },
                { typeof(T7), new ComponentArray<T7>().With(component7) },
            },
            EntityLookup = new(new(){ { new EntityId(entity), 0} })

        };
    }
}