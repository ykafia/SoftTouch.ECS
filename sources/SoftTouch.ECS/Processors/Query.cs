using System;
using System.Collections;
using System.Collections.Generic;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;

public interface IQueryEntity
{
    ArchetypeID GetQueryType();
}
public abstract class Query
{
    protected World world;
    public ArchetypeID ID;

    public int Length
    {
        get
        {
            var sum = 0;
            for (int i = 0; i < world.Archetypes.Values.Count; i++)
                if (world.Archetypes.Values[i].IsSupersetOf(ID.Span))
                    sum += world.Archetypes.Values[i].Length;
            return sum;
        }
    }

    public Query() { }
    public Query(World w)
    {
        world = w;
    }


    public abstract Query With(World w);
    
    internal CmpIdx ComputeIndex(int index)
    {
        var csum = 0;
        for (int i = 0; i < world.Archetypes.Values.Count; i++)
        {
            if (world.Archetypes.Values[i].IsSupersetOf(ID.Span))
                csum += world.Archetypes.Values[i].Length;
            if (csum > index)
            {
                return new(world.Archetypes.Values[i], world.Archetypes.Values[i].Length - (csum - index));
            }
        }
        throw new IndexOutOfRangeException();
    }

}
public class Query<T> : Query
    where T : struct
{
    public Query()
    {
    }


    public QueryIterator<T> CreateIterator() => new QueryIterator<T>(world);

    

    public Components<T> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new Components<T>(ref idx.Archetype.GetComponentSpan<T>()[idx.Index]);
        }
    }
    // public void Get(int i, ref T component1)
    // {
    //     var idx = ComputeIndex(i);
    //     component1 = ref idx.Archetype.GetComponentSpan<T>()[idx.Index];
    // }

    public override Query<T> With(World w)
    {
        world = w;
        ID = new(typeof(T));
        return (Query<T>)this;
    }

    
}
public class Query<T1, T2> : Query
    where T1 : struct
    where T2 : struct
{

    public QueryIterator<T1, T2> CreateIterator() => new QueryIterator<T1, T2>(world);

    public override Query<T1, T2> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2));
        return this;
    }
    public Components<T1, T2> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(ref idx.Archetype.GetComponentSpan<T1>()[idx.Index], ref idx.Archetype.GetComponentSpan<T2>()[idx.Index]);
        }
    }

    // public void Get(int i, out T1 component1, out T2 component2)
    // {
    //     var idx = ComputeIndex(i);
    //     component1 = idx.Archetype.GetComponentSpan<T1>()[idx.Index];
    //     component2 = idx.Archetype.GetComponentSpan<T2>()[idx.Index];
    // }
    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsSupersetOf(ID.Span))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }
}
public class Query<T1, T2, T3> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    public QueryIterator<T1, T2, T3> CreateIterator() => new QueryIterator<T1, T2, T3>(world);


    public override Query<T1, T2, T3> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3));
        return this;
    }
    public Components<T1, T2, T3> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                ref idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T3>()[idx.Index]
            );
        }
    }

    // public void Get(int i, out T1 component1, out T2 component2, out T3 component3)
    // {
    //     var idx = ComputeIndex(i);
    //     component1 = idx.Archetype.GetComponentSpan<T1>()[idx.Index];
    //     component2 = idx.Archetype.GetComponentSpan<T2>()[idx.Index];
    //     component3 = idx.Archetype.GetComponentSpan<T3>()[idx.Index];
    // }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsSupersetOf(ID.Span))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }
}
public class Query<T1, T2, T3, T4> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    public QueryIterator<T1, T2, T3, T4> CreateIterator() => new QueryIterator<T1, T2, T3, T4>(world);


    public override Query<T1, T2, T3, T4> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        return this;
    }
    public Components<T1, T2, T3, T4> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                ref idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T4>()[idx.Index]
            );
        }
    }

    // public void Get(int i, out T1 component1, out T2 component2, out T3 component3, out T4 component4)
    // {
    //     var idx = ComputeIndex(i);
    //     component1 = idx.Archetype.GetComponentSpan<T1>()[idx.Index];
    //     component2 = idx.Archetype.GetComponentSpan<T2>()[idx.Index];
    //     component3 = idx.Archetype.GetComponentSpan<T3>()[idx.Index];
    //     component4 = idx.Archetype.GetComponentSpan<T4>()[idx.Index];
    // }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsSupersetOf(ID.Span))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }
}
public class Query<T1, T2, T3, T4, T5> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    public QueryIterator<T1, T2, T3, T4, T5> CreateIterator() => new QueryIterator<T1, T2, T3, T4, T5>(world);

    public override Query<T1, T2, T3, T4, T5> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        return this;
    }

    public Components<T1, T2, T3, T4, T5> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                ref idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T4>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T5>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsSupersetOf(ID.Span))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }
}
public class Query<T1, T2, T3, T4, T5, T6> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
{
    public QueryIterator<T1, T2, T3, T4, T5, T6> CreateIterator() => new QueryIterator<T1, T2, T3, T4, T5, T6>(world);
    
    public override Query<T1, T2, T3, T4, T5, T6> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        return this;
    }

    public Components<T1, T2, T3, T4, T5, T6> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                ref idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T4>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T5>()[idx.Index],
                ref idx.Archetype.GetComponentSpan<T6>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsSupersetOf(ID.Span))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }


}