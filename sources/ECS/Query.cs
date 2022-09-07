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
    }
    public class Query<T> : Query, IEnumerable<(ArchetypeRecord,Ref<T>)>
        where T : struct
    {
        public Query(World w) : base(w){}

        public IEnumerator<(ArchetypeRecord,Ref<T>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T>(arch.GetComponentArray<T>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    public class Query<T1,T2> : Query<T1>, IEnumerable<(ArchetypeRecord,Ref<T1>,Ref<T2>)>
        where T1 : struct
        where T2 : struct
    {
        public Query(World w) : base(w){}

        new public IEnumerator<(ArchetypeRecord,Ref<T1>,Ref<T2>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T1),typeof(T2));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T1>(arch.GetComponentArray<T1>(), world[arch.EntityID[i]]),
                            new Ref<T2>(arch.GetComponentArray<T2>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    public class Query<T1,T2,T3> : Query<T1,T2>, IEnumerable<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        public Query(World w) : base(w){}

        new public IEnumerator<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T1),typeof(T2),typeof(T3));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T1>(arch.GetComponentArray<T1>(), world[arch.EntityID[i]]),
                            new Ref<T2>(arch.GetComponentArray<T2>(), world[arch.EntityID[i]]),
                            new Ref<T3>(arch.GetComponentArray<T3>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4> : Query<T1,T2,T3>, IEnumerable<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        public Query(World w) : base(w){}

        new public IEnumerator<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T1),typeof(T2),typeof(T3),typeof(T4));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T1>(arch.GetComponentArray<T1>(), world[arch.EntityID[i]]),
                            new Ref<T2>(arch.GetComponentArray<T2>(), world[arch.EntityID[i]]),
                            new Ref<T3>(arch.GetComponentArray<T3>(), world[arch.EntityID[i]]),
                            new Ref<T4>(arch.GetComponentArray<T4>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4,T5> : Query<T1,T2,T3,T4>, IEnumerable<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>,Ref<T5>)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        public Query(World w) : base(w){}

        new public IEnumerator<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>,Ref<T5>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T1),typeof(T2),typeof(T3),typeof(T4),typeof(T5));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T1>(arch.GetComponentArray<T1>(), world[arch.EntityID[i]]),
                            new Ref<T2>(arch.GetComponentArray<T2>(), world[arch.EntityID[i]]),
                            new Ref<T3>(arch.GetComponentArray<T3>(), world[arch.EntityID[i]]),
                            new Ref<T4>(arch.GetComponentArray<T4>(), world[arch.EntityID[i]]),
                            new Ref<T5>(arch.GetComponentArray<T5>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
    public class Query<T1,T2,T3,T4,T5,T6> : Query<T1,T2,T3,T4,T6>, IEnumerable<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>,Ref<T5>,Ref<T6>)>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        public Query(World w) : base(w){}

        new public IEnumerator<(ArchetypeRecord,Ref<T1>,Ref<T2>,Ref<T3>,Ref<T4>,Ref<T5>,Ref<T6>)> GetEnumerator()
        {
            var id = new ArchetypeID(typeof(T1),typeof(T2),typeof(T3),typeof(T4),typeof(T5), typeof(T6));
            foreach(var arch in world.Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(id))
                    for(int i = 0; i<arch.Length; i++)
                        yield return (
                            world.Entities[arch.EntityID[i]],
                            new Ref<T1>(arch.GetComponentArray<T1>(), world[arch.EntityID[i]]),
                            new Ref<T2>(arch.GetComponentArray<T2>(), world[arch.EntityID[i]]),
                            new Ref<T3>(arch.GetComponentArray<T3>(), world[arch.EntityID[i]]),
                            new Ref<T4>(arch.GetComponentArray<T4>(), world[arch.EntityID[i]]),
                            new Ref<T5>(arch.GetComponentArray<T5>(), world[arch.EntityID[i]]),
                            new Ref<T6>(arch.GetComponentArray<T6>(), world[arch.EntityID[i]])
                        );
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
    }
}