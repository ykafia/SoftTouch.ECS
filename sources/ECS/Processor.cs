namespace ECSharp
{
    public abstract class Processor : ProcessorBase
    {
        public virtual void Update() { }
    }
    public partial class Processor<Q> : Processor where Q : Query
    {
        protected readonly Q query1;

        public Processor()
        {
            query1 = (Q)new Query(World);
        }
    }
    public partial class Processor<Q1, Q2> : Processor<Q1> where Q2 : Query
        where Q1 : Query
    {
        protected readonly Q2 query2;

        public Processor() : base()
        {
            query2 = (Q2)new Query(World);
        }

    }
    public partial class Processor<Q1, Q2, Q3> : Processor<Q1, Q2> 
    where Q1 : Query
    where Q2 : Query
    where Q3 : Query

    {
        protected readonly Q3 query3;

        public Processor() : base()
        {
            query3 = (Q3)new Query(World);
        }
    }
    public partial class Processor<Q1, Q2, Q3, Q4> : Processor<Q1,Q2,Q3> 
    where Q1 : Query
    where Q2 : Query
    where Q3 : Query
    where Q4 : Query
    {
        protected readonly Q4 query4;

        public Processor() : base()
        {
            query4 = (Q4)new Query(World);
        }
    }
}