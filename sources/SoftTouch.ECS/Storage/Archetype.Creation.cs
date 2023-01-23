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
            long entity,
            in T1 component1
        )
        where T1 : struct
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
            },
            EntityLookup = new(new(){ { entity, 0} })
            
        };
    }

    public static Archetype Create<T1, T2>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2
        )
        where T1 : struct
        where T2 : struct
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }


    public static Archetype Create<T1, T2, T3>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2,
            in T3 component3
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
                { typeof(T3), new ComponentList<T3>{component3} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        return new Archetype
        {
            ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
                { typeof(T3), new ComponentList<T3>{component3} },
                { typeof(T4), new ComponentList<T4>{component4} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
                { typeof(T3), new ComponentList<T3>{component3} },
                { typeof(T4), new ComponentList<T4>{component4} },
                { typeof(T5), new ComponentList<T5>{component5} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5, T6>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
                { typeof(T3), new ComponentList<T3>{component3} },
                { typeof(T4), new ComponentList<T4>{component4} },
                { typeof(T5), new ComponentList<T5>{component5} },
                { typeof(T6), new ComponentList<T6>{component6} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }

    public static Archetype Create<T1, T2, T3, T4, T5, T6, T7>(
            World world, 
            in ArchetypeID id, 
            long entity,
            in T1 component1,
            in T2 component2,
            in T3 component3,
            in T4 component4,
            in T5 component5,
            in T6 component6,
            in T7 component7
        )
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
    {
        return new Archetype
        {
                        ID = id,
            World = world,
            Storage = new()
            {
                { typeof(T1), new ComponentList<T1>{component1} },
                { typeof(T2), new ComponentList<T2>{component2} },
                { typeof(T3), new ComponentList<T3>{component3} },
                { typeof(T4), new ComponentList<T4>{component4} },
                { typeof(T5), new ComponentList<T5>{component5} },
                { typeof(T6), new ComponentList<T6>{component6} },
                { typeof(T7), new ComponentList<T7>{component7} },
            },
            EntityLookup = new(new(){ { entity, 0} })

        };
    }
}