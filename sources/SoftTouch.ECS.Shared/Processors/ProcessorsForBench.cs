using System.Runtime.InteropServices;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Shared.Components;

namespace SoftTouch.ECS.Shared.Processors;

public class Processor1 : Processor<Query<HealthComponent, NoFilter>>
{
    Random rand = new Random();

    public Processor1(World world) : base(world)
    {
    }

    public override void Update()
    {
        // for (int i = 0; i < Entities1.Length; i++)
        // {
        //     Entities1[i].Component1.LifePoints = 247;
        // }
    }
}
public class Processor2 : Processor<Query<HealthComponent, NoFilter>>
{
    Random rand = new Random();

    public Processor2(World world) : base(world)
    {
    }

    public override void Update()
    {
        // for (int i = 0; i < Entities1.Length; i++)
        // {
        //     Entities1[i].Component1.LifePoints = 247;
        // }
    }
}
//public class Processor3 : Processor<Query<NameComponent, HealthComponent>>
//{
//    Random rand = new Random();

//    public Processor3(World world) : base(world)
//    {
//    }

//    public override void Update()
//    {
//        // for (int i = 0; i < Entities1.Length; i++)
//        // {
//        //     Entities1[i].Component1.LifePoints = 247;
//        // }
//    }
//}