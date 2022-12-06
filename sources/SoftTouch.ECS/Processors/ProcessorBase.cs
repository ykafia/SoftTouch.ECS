namespace SoftTouch.ECS;

public abstract class ProcessorBase
{
    public Schedule Schedule { get; set; }
    public World World { get; set; }
}

// public class ProcessorA<T> where T : Query
// {
//     protected T Query1;
// }

// public class SomProcessor : ProcessorA<Query<int>>
// {
//     public void fun()
//     {
//         var x = Query1;
//     }
// }