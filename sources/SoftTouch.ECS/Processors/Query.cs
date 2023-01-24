using System;
using System.Collections;
using System.Collections.Generic;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Storage;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SoftTouch.ECS;

public abstract class Query
{
    protected World world;
    public ArchetypeID ID;
    protected WorldCommands Commands => world.Commands;

    public virtual int Length
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


    public virtual Query With(World w)
    {
        world = w;
        return this;
    }
}




public class Query<T> : Query
    where T : struct
{
    public Query()
    {
    }


    public QueryEnumerator<T> GetEnumerator() => new(world);


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

    public QueryEnumerator<T1, T2> GetEnumerator() => new(world);

    public override Query<T1, T2> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2));
        return this;
    }
}
public class Query<T1, T2, T3> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
{
    public QueryEnumerator<T1, T2, T3> GetEnumerator() => new(world);


    public override Query<T1, T2, T3> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3));
        return this;
    }
}
public class Query<T1, T2, T3, T4> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
{
    public QueryEnumerator<T1, T2, T3, T4> GetEnumerator() => new(world);


    public override Query<T1, T2, T3, T4> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        return this;
    }
}
public class Query<T1, T2, T3, T4, T5> : Query
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
{
    public QueryEnumerator<T1, T2, T3, T4, T5> GetEnumerator() => new(world);

    public override Query<T1, T2, T3, T4, T5> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));
        return this;
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
    public QueryEnumerator<T1, T2, T3, T4, T5, T6> GetEnumerator() => new(world);

    public override Query<T1, T2, T3, T4, T5, T6> With(World w)
    {
        world = w;
        ID = new ArchetypeID(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));
        return this;
    }
}