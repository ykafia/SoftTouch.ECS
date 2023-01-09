namespace SoftTouch.ECS
{
    public abstract class Processor : ProcessorBase
    {
        public virtual void Update() { }
    }

    public partial class Processor<Q> : Processor where Q : Query, new()
    {
        protected readonly Q entities1;
        public Processor()
        {
            entities1 = (Q)new Q().With(World);
        }
    }
    public partial class Processor<Q1, Q2> : Processor<Q1>
        where Q1 : Query, new()
        where Q2 : Query, new()

    {
        protected readonly Q2 entities2;

        public Processor() : base()
        {
            entities2 = (Q2)new Q2().With(World);
        }

    }
    public partial class Processor<Q1, Q2, Q3> : Processor<Q1, Q2>
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()

    {
        protected readonly Q3 entities3;

        public Processor() : base()
        {
            entities3 = (Q3)new Q3().With(World);
        }
    }
    public partial class Processor<Q1, Q2, Q3, Q4> : Processor<Q1, Q2, Q3>
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()
        where Q4 : Query, new()
    {
        protected readonly Q4 entities4;

        public Processor() : base()
        {
            entities4 = (Q4)new Q4().With(World);
        }
    }
}