// See https://aka.ms/new-console-template for more information
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Attributes;

namespace SoftTouch.ECS.Example;


public static class BundledFunctions
{
    [Bundle("Hello")]
    public static void MyFunction(Query<int> entities)
    {
        entities.ForEach((ref int age) => age += 1);
    }
    
    [Bundle("Hello")]
    public static void MyNotWorkingFunction(Query<int> entities, Query<float> somethingElse)
    {
        entities.ForEach((ref int age) => age += 1);
    }
}

public class HealthProcessor : Processor<Query<HealthComponent>>
{
    Random rand = new Random();

    public HealthProcessor(World world) : base(world)
    {
    }

    public override void Update()
    {
        foreach(var e in Query)
        {
            // Do something;
        }
    }
}

//public class PlayerProcessor : Processor<Query<NameComponent, HealthComponent>>
//{
//    Random rand = new Random();

//    public PlayerProcessor(World world) : base(world)
//    {
//    }

//    public override void Update()
//    {
//        foreach(var e in Query)
//        {
//            e.Set<NameComponent>("Bobby Kane");
//        }
//    }
//}


public class AsyncProcessor : Processor<Query<int>>
{
    public AsyncProcessor(World world) : base(world)
    {
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}
