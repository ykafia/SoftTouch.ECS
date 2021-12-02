namespace WonkECS
{
    public partial class Processor
    {
        public virtual void Update(World Manager)
        {
            // Manager.QueryArchetypes(QueryEntity.GetQueryType());
        }
    }
    public partial class Processor<Q> : Processor where Q : IQueryEntity, new()
    {
        public Q QueryEntity = new();
        
    }
    public partial class Processor<Q1,Q2> : Processor
        where Q1 : IQueryEntity, new()
        where Q2 : IQueryEntity, new()
    {
        public Q1 QueryEntity1 = new();
        public Q2 QueryEntity2 = new();
    }
    public partial class Processor<Q1,Q2,Q3> : Processor
        where Q1 : IQueryEntity, new()
        where Q2 : IQueryEntity, new()
        where Q3 : IQueryEntity, new()

    {
        public Q1 QueryEntity1 = new();
        public Q2 QueryEntity2 = new();
        public Q3 QueryEntity3 = new();
    }
    public partial class Processor<Q1,Q2,Q3,Q4> : Processor
        where Q1 : IQueryEntity, new()
        where Q2 : IQueryEntity, new()
        where Q3 : IQueryEntity, new()
        where Q4 : IQueryEntity, new()


    {
        public Q1 QueryEntity1 = new();
        public Q2 QueryEntity2 = new();
        public Q3 QueryEntity3 = new();
        public Q4 QueryEntity4 = new();
    }
}