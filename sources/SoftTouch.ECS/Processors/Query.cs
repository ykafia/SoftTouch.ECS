using System;
using System.Collections;
using System.Collections.Generic;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS;

public interface IQueryEntity
{
    ArchetypeID GetQueryType();
}
public abstract class Query
{
    protected World world;
    public Query() { }
    public Query(World w)
    {
        world = w;
    }



    protected ArchetypeID id;
    public abstract Query With(World w);

}
public class Query<T> : Query
    where T : struct
{
    public Query()
    {
    }

    public Components<T> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(idx.Archetype.GetComponentSpan<T>()[idx.Index]);
        }
    }

    public override Query<T> With(World w)
    {
        world = w;
        id = new(typeof(T));
        return (Query<T>)this;
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T)))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }
}
public class Query<T1, T2> : Query
    where T1 : struct
    where T2 : struct
{

    public override Query<T1, T2> With(World w)
    {
        world = w;
        id = new ArchetypeID(typeof(T1), typeof(T2));
        return this;
    }
    public Components<T1, T2> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(idx.Archetype.GetComponentSpan<T1>()[idx.Index], idx.Archetype.GetComponentSpan<T2>()[idx.Index]);
        }
    }
    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T1), typeof(T2)))
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
    public override Query<T1, T2, T3> With(World w)
    {
        world = w;
        id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3));
        return this;
    }
    public Components<T1, T2, T3> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                idx.Archetype.GetComponentSpan<T3>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T1), typeof(T2), typeof(T3)))
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
    public override Query<T1, T2, T3, T4> With(World w)
    {
        world = w;
        id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        return this;
    }
    public Components<T1, T2, T3, T4> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                idx.Archetype.GetComponentSpan<T4>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T1), typeof(T2), typeof(T3), typeof(T4)))
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
    public override Query<T1, T2, T3, T4, T5> With(World w)
    {
        world = w;
        id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        return this;
    }

    public Components<T1, T2, T3, T4, T5> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                idx.Archetype.GetComponentSpan<T4>()[idx.Index],
                idx.Archetype.GetComponentSpan<T5>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)))
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
    public override Query<T1, T2, T3, T4, T5, T6> With(World w)
    {
        world = w;
        id = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        return this;
    }

    public Components<T1, T2, T3, T4, T5,T6> this[int i]
    {
        get
        {
            var idx = ComputeIndex(i);
            return new(
                idx.Archetype.GetComponentSpan<T1>()[idx.Index],
                idx.Archetype.GetComponentSpan<T2>()[idx.Index],
                idx.Archetype.GetComponentSpan<T3>()[idx.Index],
                idx.Archetype.GetComponentSpan<T4>()[idx.Index],
                idx.Archetype.GetComponentSpan<T5>()[idx.Index],
                idx.Archetype.GetComponentSpan<T6>()[idx.Index]
            );
        }
    }

    CmpIdx ComputeIndex(int i)
    {
        var csum = 0;
        foreach (var arch in world.Archetypes.Values)
        {
            if (arch.IsStrictSubsetOf(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)))
                csum += arch.Length;
            if (csum > i)
            {
                return new(arch, arch.Length - (csum - i));
            }
        }
        throw new IndexOutOfRangeException();
    }


}