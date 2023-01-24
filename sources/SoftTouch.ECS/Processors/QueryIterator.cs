﻿using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

public ref struct QueryEnumerator<T1>
    where T1 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}

public ref struct QueryEnumerator<T1, T2>
    where T1 : struct
    where T2 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}


public ref struct QueryEnumerator<T1, T2, T3>
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2, T3> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}

public ref struct QueryEnumerator<T1, T2, T3, T4>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2, T3, T4> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}
public ref struct QueryEnumerator<T1, T2, T3, T4, T5>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2, T3, T4, T5> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}

public ref struct QueryEnumerator<T1, T2, T3, T4, T5, T6>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2, T3, T4, T5, T6> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5, T6>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5, T6>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}

public ref struct QueryEnumerator<T1, T2, T3, T4, T5, T6, T7>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
    where T7 : struct
{
    World world;
    int archIdx;
    int eIdx;

    public RefEntity<T1, T2, T3, T4, T5, T6, T7> Current =>
            new(world.Archetypes.GetValueAtIndex(archIdx).EntityLookup.GetEntityId(eIdx), world);

    public QueryEnumerator(World world)
    {
        this.world = world;
        archIdx = -1;
        eIdx = -1;
    }

    public bool MoveNext()
    {
        while (archIdx < 0 || !world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5, T6,T7>() || world.Archetypes.GetValueAtIndex(archIdx).Length == 0)
            archIdx += 1;
        eIdx += 1;
        if (eIdx >= world.Archetypes.GetValueAtIndex(archIdx).Length)
        {
            archIdx += 1;
            while (true)
            {
                if (archIdx >= world.Archetypes.Count)
                {
                    //Reset();
                    return false;
                }
                if (world.Archetypes.GetValueAtIndex(archIdx).Length > 0 && world.Archetypes.GetValueAtIndex(archIdx).IsSupersetOf<T1, T2, T3, T4, T5, T6, T7>())
                    break;
                archIdx += 1;
            }
            eIdx = 0;
        }
        if (archIdx >= world.Archetypes.Count)
        {
            //Reset();
            return false;
        }
        return true;
    }

}
