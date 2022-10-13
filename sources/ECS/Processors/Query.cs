using System;
using System.Collections;
using System.Collections.Generic;
using ECSharp.Arrays;

namespace ECSharp
{
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
        public IEnumerable<Archetype> QueriedArchetypes
        {
            get
            {
                if (world is not null)
                    return world.QueryArchetypes(id);
                else return Enumerable.Empty<Archetype>();
            }
        }

        public abstract Query With(World w);

    }
    public class Query<T> : Query
        where T : struct
    {
        public Query()
        {
        }

        public override Query<T> With(World w)
        {
            world = w;
            id = new(typeof(T));
            return (Query<T>)this;
        }
    }
    public class Query<T1, T2> : Query<T1>
        where T1 : struct
        where T2 : struct
    {

        public override Query<T1, T2> With(World w)
        {
            world = w;
            id = new ArchetypeID(typeof(T1), typeof(T2));
            return this;
        }
    }
    public class Query<T1, T2, T3> : Query<T1, T2>
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
    }
    public class Query<T1, T2, T3, T4> : Query<T1, T2, T3>
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
    }
    public class Query<T1, T2, T3, T4, T5> : Query<T1, T2, T3, T4>
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
    }
    public class Query<T1, T2, T3, T4, T5, T6> : Query<T1, T2, T3, T4, T6>
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

        
    }
}