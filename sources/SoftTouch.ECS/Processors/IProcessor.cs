namespace SoftTouch.ECS;

public interface IProcessor
{
    public void Update();
}


public interface IProcessor<W> : IProcessor
    where W : World

{
    public W World { get; set; }
    public void WithWorld(W world);
}
public interface IProcessor<W, Q1> : IProcessor<W>
    where W : World
    where Q1 : Query
{

}
public interface IProcessor<W, Q1, Q2> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
{

}
public interface IProcessorr<W, Q1, Q2, Q3> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
{

}
public interface IProcessor<W, Q1, Q2, Q3, Q4> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
    where Q4 : IQuery<W>
{

}
public interface IProcessor<W, Q1, Q2, Q3, Q4, Q5> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
    where Q4 : IQuery<W>
    where Q5 : IQuery<W>
{

}
public interface IProcessor<W, Q1, Q2, Q3, Q4, Q5, Q6> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
    where Q4 : IQuery<W>
    where Q5 : IQuery<W>
    where Q6 : IQuery<W>
{

}
public interface IProcessor<W, Q1, Q2, Q3, Q4, Q5, Q6, Q7> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
    where Q4 : IQuery<W>
    where Q5 : IQuery<W>
    where Q6 : IQuery<W>
    where Q7 : IQuery<W>
{

}
public interface IProcessor<W, Q1, Q2, Q3, Q4, Q5, Q6, Q7, Q8> : IProcessor<W>
    where W : World
    where Q1 : IQuery<W>
    where Q2 : IQuery<W>
    where Q3 : IQuery<W>
    where Q4 : IQuery<W>
    where Q5 : IQuery<W>
    where Q6 : IQuery<W>
    where Q7 : IQuery<W>
    where Q8 : IQuery<W>
{

}