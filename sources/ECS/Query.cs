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
    public class Query 
    {
        protected World world;
        public Query(World w)
        {
            world = w;
        }

        protected ArchetypeID id;
        public IEnumerable<Archetype> QueriedArchetypes => world.Archetypes.Values.Where(x=> x.ID.IsSupersetOf(id));

    }
    public class Query<T> : Query, IEnumerable<(ArchetypeRecord,T)>
        where T : struct
    {
        public Query(World w) : base(w)
        {
            id = new ArchetypeID(typeof(T));
        }

        public IEnumerator<(ArchetypeRecord,T)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T>()
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Query<T1,T2> : Query<T1>, IEnumerable<(ArchetypeRecord,T1,T2)>
        where T1 : struct
        where T2 : struct
    {
        public Query(World w) : base(w)
        {
            id = new ArchetypeID(typeof(T1),typeof(T2));
        }

        new public IEnumerator<(ArchetypeRecord,T1,T2)> GetEnumerator()
        {
            foreach(var arch in QueriedArchetypes)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T1>(),
                            world.Entities[arch.EntityID[i]].Get<T2>()
                            
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Query<T1,T2,T3> : Query<T1,T2>, IEnumerable<(ArchetypeRecord,T1,T2,T3)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        public Query(World w) : base(w)
        {
            id = new(typeof(T1),typeof(T2),typeof(T3));
        }

        new public IEnumerator<(ArchetypeRecord,T1,T2,T3)> GetEnumerator()
        {
            foreach(var arch in QueriedArchetypes)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T1>(),
                            world.Entities[arch.EntityID[i]].Get<T2>(),
                            world.Entities[arch.EntityID[i]].Get<T3>()
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4> : Query<T1,T2,T3>, IEnumerable<(ArchetypeRecord,T1,T2,T3,T4)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        public Query(World w) : base(w)
        {
            id = new(typeof(T1),typeof(T2),typeof(T3), typeof(T4));
        }

        new public IEnumerator<(ArchetypeRecord,T1,T2,T3,T4)> GetEnumerator()
        {
            foreach(var arch in QueriedArchetypes)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T1>(),
                            world.Entities[arch.EntityID[i]].Get<T2>(),
                            world.Entities[arch.EntityID[i]].Get<T3>(),
                            world.Entities[arch.EntityID[i]].Get<T4>()
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4,T5> : Query<T1,T2,T3,T4>, IEnumerable<(ArchetypeRecord,T1,T2,T3,T4,T5)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        public Query(World w) : base(w)
        {
            id = new(typeof(T1),typeof(T2),typeof(T3), typeof(T4), typeof(T5));
        }

        new public IEnumerator<(ArchetypeRecord,T1,T2,T3,T4,T5)> GetEnumerator()
        {
            foreach(var arch in QueriedArchetypes)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T1>(),
                            world.Entities[arch.EntityID[i]].Get<T2>(),
                            world.Entities[arch.EntityID[i]].Get<T3>(),
                            world.Entities[arch.EntityID[i]].Get<T4>(),
                            world.Entities[arch.EntityID[i]].Get<T5>()
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4,T5,T6> : Query<T1,T2,T3,T4,T6>, IEnumerable<(ArchetypeRecord,T1,T2,T3,T4,T5,T6)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        public Query(World w) : base(w)
        {
            id = new(typeof(T1),typeof(T2),typeof(T3), typeof(T4),typeof(T5), typeof(T6));
        }

        new public IEnumerator<(ArchetypeRecord,T1,T2,T3,T4,T5,T6)> GetEnumerator()
        {
            
            foreach(var arch in QueriedArchetypes)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            world.Entities[arch.EntityID[i]].Get<T1>(),
                            world.Entities[arch.EntityID[i]].Get<T2>(),
                            world.Entities[arch.EntityID[i]].Get<T3>(),
                            world.Entities[arch.EntityID[i]].Get<T4>(),
                            world.Entities[arch.EntityID[i]].Get<T5>(),
                            world.Entities[arch.EntityID[i]].Get<T6>()
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}