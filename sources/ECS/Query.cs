using System;
using System.Collections.Generic;

namespace ECSharp
{
    public interface IQueryEntity
    {
        ArchetypeID GetQueryType();
    }
    
    public class QueryEntity<T1> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1)});

        public ArchetypeID GetQueryType() => QueryType;
    }
    public class QueryEntity<T1,T2> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3,T4> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3), typeof(T4)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3,T4,T5> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3,T4,T5,T6> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3,T4,T5,T6,T7> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)});
        public ArchetypeID GetQueryType() => QueryType;

    }
    public class QueryEntity<T1,T2,T3,T4,T5,T6,T7,T8> : IQueryEntity
    {
        public static readonly ArchetypeID QueryType = new(new List<Type>{typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)});
        public ArchetypeID GetQueryType() => QueryType;

    }
}