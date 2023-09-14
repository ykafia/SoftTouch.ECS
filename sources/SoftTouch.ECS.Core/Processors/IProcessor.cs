namespace SoftTouch.ECS;

public interface IProcessor
{
    public void Update();
}


public interface IProcessor : IProcessor

{
    public W World { get; set; }
    public void WithWorld(W world);
}
public interface IProcessor<Q1> : IProcessor
    where Q1 : Query
{

}
public interface IProcessor<Q1, Q2> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3, Q4> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
    where Q4 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3, Q4, Q5> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
    where Q4 : IQuery
    where Q5 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
    where Q4 : IQuery
    where Q5 : IQuery
    where Q6 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6, Q7> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
    where Q4 : IQuery
    where Q5 : IQuery
    where Q6 : IQuery
    where Q7 : IQuery
{

}
public interface IProcessor<Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8> : IProcessor
    where Q1 : IQuery
    where Q2 : IQuery
    where Q3 : IQuery
    where Q4 : IQuery
    where Q5 : IQuery
    where Q6 : IQuery
    where Q7 : IQuery
    where Q8 : IQuery
{

}