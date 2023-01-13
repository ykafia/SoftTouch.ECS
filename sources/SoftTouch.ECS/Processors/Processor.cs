namespace SoftTouch.ECS
{
    public abstract class Processor : ProcessorBase
    {
        public Processor() { }

        public virtual Processor With(World world)
        {
            World = world;
            return this;
        }
        public virtual void Update() { }

    }

    public partial class Processor<Q> : Processor where Q : Query, new()
    {
        protected Q Entities1 { get; private set; }
        
        public Processor(){}

        public override Processor With(World world)
        {
            World = world;
            Entities1 = (Q)new Q().With(World);
            return this;
        }
    }
    public partial class Processor<Q1, Q2> : Processor
        where Q1 : Query, new()
        where Q2 : Query, new()

    {
        protected Q1 Entities1 {get;private set;}
        protected Q2 Entities2 {get;private set;}

        public override Processor With(World world)
        {
            World = world;
            Entities1 = (Q1)new Q1().With(World);
            Entities2 = (Q2)new Q2().With(World);
            return this;
        }

    }
    public partial class Processor<Q1, Q2, Q3> : Processor
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()

    {
        protected Q1 Entities1 {get;private set;}
        protected Q2 Entities2 {get;private set;}
        protected Q3 Entities3 {get;private set;}

        public override Processor With(World world)
        {
            World = world;
            Entities1 = (Q1)new Q1().With(World);
            Entities2 = (Q2)new Q2().With(World);
            Entities3 = (Q3)new Q3().With(World);
            return this;
        }
    }
    public partial class Processor<Q1, Q2, Q3, Q4> : Processor
        where Q1 : Query, new()
        where Q2 : Query, new()
        where Q3 : Query, new()
        where Q4 : Query, new()
    {
        protected Q1 Entities1 {get;private set;}
        protected Q2 Entities2 {get;private set;}
        protected Q3 Entities3 {get;private set;}
        protected Q4 Entities4 {get;private set;}

        public override Processor With(World world)
        {
            World = world;
            Entities1 = (Q1)new Q1().With(World);
            Entities2 = (Q2)new Q2().With(World);
            Entities3 = (Q3)new Q3().With(World);
            Entities4 = (Q4)new Q4().With(World);
            return this;
        }
    }
}