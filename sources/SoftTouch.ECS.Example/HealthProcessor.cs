// See https://aka.ms/new-console-template for more information
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS;

namespace SoftTouch.ECS.Example;

public class HealthProcessor : Processor<Query<HealthComponent>>
{
    Random rand = new Random();

    public override void Update()
    {
        foreach(var e in Entities1)
        {
            // Do something;
        }
    }
}

public class PlayerProcessor : Processor<Query<NameComponent, HealthComponent>>
{
    Random rand = new Random();

    public override void Update()
    {
        foreach(var e in Entities1)
        {
            var (name, health) = e;
            name.Name = "Bob Kane";
        }
    }
}


public class AsyncProcessor : Processor<Query<int>>
{
    
}
