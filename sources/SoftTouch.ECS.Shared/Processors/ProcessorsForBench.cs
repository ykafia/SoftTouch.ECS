using System.Runtime.InteropServices;
using SoftTouch.ECS.Components;

namespace SoftTouch.ECS.Processors;

public class Processor1 : Processor<Query<HealthComponent>>
{
    Random rand = new Random();

    public Processor1(World world)
    {
    }

    public override void Update()
    {
        // foreach((var e, var h) in query1)
        // {

        // }
    }
}
public class Processor2 : Processor<Query<HealthComponent>>
{
    public Processor2(World world)
    {
    }

    public override void Update()
    {
        // foreach((var e, var health) in query1)
        //     e.Set(health with {LifePoints = 100});
    }
}
public class Processor3 : Processor<Query<NameComponent, HealthComponent>>
{
    public Processor3(World world)
    {
    }

    public override void Update()
    {
        // foreach((var e, var n, var h) in query1)
        // {
        //     e.Set(n with {Name = "Lola"});
        //     e.Set(h with {LifePoints = 100});
        // }
    }
}