using System.Runtime.InteropServices;
using SoftTouch.ECS.Shared.Components;

namespace SoftTouch.ECS.Shared.Processors;

public class Processor1 : Processor<Query<HealthComponent>>
{
    public Processor1() { }
    Random rand = new Random();

    public override void Update()
    {
        // for (int i = 0; i < Entities1.Length; i++)
        // {
        //     Entities1[i].Component1.LifePoints = 247;
        // }
    }
}
public class Processor2 : Processor<Query<HealthComponent>>
{
    Random rand = new Random();

    public Processor2() { }

    public override void Update()
    {
        // for (int i = 0; i < Entities1.Length; i++)
        // {
        //     Entities1[i].Component1.LifePoints = 247;
        // }
    }
}
public class Processor3 : Processor<Query<NameComponent, HealthComponent>>
{
    Random rand = new Random();

    public Processor3() { }
    
    public override void Update()
    {
        // for (int i = 0; i < Entities1.Length; i++)
        // {
        //     Entities1[i].Component1.LifePoints = 247;
        // }
    }
}