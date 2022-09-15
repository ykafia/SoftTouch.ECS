namespace ECSharp;


public abstract class ProcessorAsync : ProcessorBase
{
    public virtual async Task Execute(){}
}
public partial class ProcessorAsync<Q> : ProcessorAsync where Q : Query, new()
{
    readonly Q query1;

    public ProcessorAsync()
    {
        query1 = (Q) new Q().With(World);
    }

    // public IEnumerable<Archetype> Query1 => World.QueryArchetypes(queryEntity.GetQueryType());
}
public partial class ProcessorAsync<Q1, Q2> : ProcessorAsync
    where Q1 : IQueryEntity, new()
    where Q2 : IQueryEntity, new()
{
    readonly Q1 queryEntity1 = new();
    readonly Q2 queryEntity2 = new();

    // public IEnumerable<Archetype> Query1 => World.QueryArchetypes(queryEntity1.GetQueryType());
    // public IEnumerable<Archetype> Query2 => World.QueryArchetypes(queryEntity2.GetQueryType());

}
public partial class ProcessorAsync<Q1, Q2, Q3> : ProcessorAsync
    where Q1 : IQueryEntity, new()
    where Q2 : IQueryEntity, new()
    where Q3 : IQueryEntity, new()

{
    readonly Q1 queryEntity1 = new();
    readonly Q2 queryEntity2 = new();
    readonly Q3 queryEntity3 = new();


    // public IEnumerable<Archetype> Query1 => World.QueryArchetypes(queryEntity1.GetQueryType());
    // public IEnumerable<Archetype> Query2 => World.QueryArchetypes(queryEntity2.GetQueryType());
    // public IEnumerable<Archetype> Query3 => World.QueryArchetypes(queryEntity3.GetQueryType());

}
public partial class ProcessorAsync<Q1, Q2, Q3, Q4> : ProcessorAsync
    where Q1 : IQueryEntity, new()
    where Q2 : IQueryEntity, new()
    where Q3 : IQueryEntity, new()
    where Q4 : IQueryEntity, new()


{
    readonly Q1 queryEntity1 = new();
    readonly Q2 queryEntity2 = new();
    readonly Q3 queryEntity3 = new();
    readonly Q3 queryEntity4 = new();


    // public IEnumerable<Archetype> Query1 => World.QueryArchetypes(queryEntity1.GetQueryType());
    // public IEnumerable<Archetype> Query2 => World.QueryArchetypes(queryEntity2.GetQueryType());
    // public IEnumerable<Archetype> Query3 => World.QueryArchetypes(queryEntity3.GetQueryType());
    // public IEnumerable<Archetype> Query4 => World.QueryArchetypes(queryEntity4.GetQueryType());

}