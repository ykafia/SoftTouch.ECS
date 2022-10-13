namespace ECSharp
{
    public abstract class Processor : ProcessorBase
    {
        public delegate void UpdateDelegate<T1>(ref T1 arg1)
            where T1 : struct;
        public delegate void UpdateDelegate<T1, T2>(T1 arg1, ref T2 arg2)
            where T1 : struct 
            where T2 : struct;
        public delegate void UpdateDelegate<T1, T2, T3>(ref T1 arg1, ref T2 arg2, ref T3 arg3)
            where T1 : struct 
            where T2 : struct
            where T3 : struct;
        public delegate void UpdateDelegate<T1, T2, T3, T4>(ref T1 arg1, ref T2 arg2, ref T3 arg3, ref T4 arg4)
            where T1 : struct 
            where T2 : struct
            where T3 : struct
            where T4 : struct;

        public virtual void Update() { }
    }

    public partial class Processor<Q> : Processor where Q : Query, new()
    {
        protected readonly Q query1;
        public Processor()
        {
            query1 = (Q)new Q().With(World);
        }
    }
    public partial class Processor<Q1, Q2> : Processor<Q1>
        where Q1 : Query, new()
        where Q2 : Query, new()

    {
        protected readonly Q2 query2;

        public Processor() : base()
        {
            query2 = (Q2)new Q2().With(World);
        }

    }
    public partial class Processor<Q1, Q2, Q3> : Processor<Q1, Q2>
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()

    {
        protected readonly Q3 query3;

        public Processor() : base()
        {
            query3 = (Q3)new Q3().With(World);
        }
    }
    public partial class Processor<Q1, Q2, Q3, Q4> : Processor<Q1, Q2, Q3>
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()
        where Q4 : Query, new()
    {
        protected readonly Q4 query4;

        public Processor() : base()
        {
            query4 = (Q4)new Q4().With(World);
        }
    }
}