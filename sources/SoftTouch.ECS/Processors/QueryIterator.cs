using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

public ref struct QueryIterator<T1>
    where T1 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent<T>(entityIndex,out component);
    }
    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(out T1 component)
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while(true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}

public ref struct QueryIterator<T1, T2>
    where T1 : struct
    where T2 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }

    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(out T1 component1, out T2 component2)
    {
        archetypes[archetypeIndex].GetComponent(entityIndex,out component1);
        archetypes[archetypeIndex].GetComponent(entityIndex,out component2);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1,T2>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while (true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1,T2>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}

public ref struct QueryIterator<T1, T2, T3>
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }
    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(out T1 component1, out T2 component2, out T3 component3)
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component1);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component2);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component3);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while (true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}


public ref struct QueryIterator<T1, T2, T3, T4>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }
    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(
        out T1 component1,
        out T2 component2,
        out T3 component3,
        out T4 component4
        )
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component1);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component2);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component3);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component4);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while (true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}


public ref struct QueryIterator<T1, T2, T3, T4, T5>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }
    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(
        out T1 component1,
        out T2 component2,
        out T3 component3,
        out T4 component4,
        out T5 component5
        )
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component1);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component2);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component3);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component4);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component5);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4, T5>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while (true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4, T5>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}

public ref struct QueryIterator<T1, T2, T3, T4, T5, T6>
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
{
    IList<Archetype> archetypes;
    int archetypeIndex;
    int entityIndex;

    public QueryIterator(World w)
    {
        archetypeIndex = -1;
        entityIndex = -1;
        archetypes = w.Archetypes.Values;
    }

    public void Get<T>(out T component)
        where T : struct
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component);
    }
    public void Set<T>(in T value)
        where T : struct
    {
        archetypes[archetypeIndex].SetComponent(entityIndex, in value);
    }

    public void Deconstruct(
        out T1 component1,
        out T2 component2,
        out T3 component3,
        out T4 component4,
        out T5 component5,
        out T6 component6
        )
    {
        archetypes[archetypeIndex].GetComponent(entityIndex, out component1);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component2);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component3);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component4);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component5);
        archetypes[archetypeIndex].GetComponent(entityIndex, out component6);
    }

    public bool Next()
    {
        while (archetypeIndex < 0 || !archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4, T5, T6>() || archetypes[archetypeIndex].Length == 0)
            archetypeIndex += 1;
        entityIndex += 1;
        if (entityIndex >= archetypes[archetypeIndex].Length)
        {
            archetypeIndex += 1;
            while (true)
            {
                if (archetypeIndex >= archetypes.Count)
                {
                    Reset();
                    return false;
                }
                if (archetypes[archetypeIndex].Length > 0 && archetypes[archetypeIndex].IsSupersetOf<T1, T2, T3, T4, T5, T6>())
                    break;
                archetypeIndex += 1;
            }
            entityIndex = 0;
        }
        if (archetypeIndex >= archetypes.Count)
        {
            Reset();
            return false;
        }
        return true;
    }

    public void Reset()
    {
        archetypeIndex = -1;
        entityIndex = -1;
    }

}


