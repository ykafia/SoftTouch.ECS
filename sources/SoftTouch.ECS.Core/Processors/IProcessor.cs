using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Processors;


public interface IProcessor

{
    public World World { get; set; }
    public void WithWorld(World world);
    public void Update();

}
public interface IProcessor<Q1> : IProcessor
    where Q1 : IWorldQuery;


public interface IProcessor<Q1, Q2> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery;


public interface IProcessor<Q1, Q2, Q3> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery;

public interface IProcessor<Q1, Q2, Q3, Q4> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery
    where Q4 : IWorldQuery;
    
public interface IProcessor<Q1, Q2, Q3, Q4, Q5> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery
    where Q4 : IWorldQuery
    where Q5 : IWorldQuery;

public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery
    where Q4 : IWorldQuery
    where Q5 : IWorldQuery
    where Q6 : IWorldQuery;
    
public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6, Q7> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery
    where Q4 : IWorldQuery
    where Q5 : IWorldQuery
    where Q6 : IWorldQuery
    where Q7 : IWorldQuery;

public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8> : IProcessor
    where Q1 : IWorldQuery
    where Q2 : IWorldQuery
    where Q3 : IWorldQuery
    where Q4 : IWorldQuery
    where Q5 : IWorldQuery
    where Q6 : IWorldQuery
    where Q7 : IWorldQuery
    where Q8 : IWorldQuery;