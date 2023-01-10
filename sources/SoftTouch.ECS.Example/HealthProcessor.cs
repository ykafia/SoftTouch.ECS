// See https://aka.ms/new-console-template for more information
using SoftTouch.ECS.Components;
using SoftTouch.ECS;

namespace SoftTouch.ECS.Example;

public class HealthProcessor : Processor<Query<HealthComponent>>
{
    Random rand = new Random();

    public override void Update()
    {
        for (int i = 0; i < Entities1.Length; i++)
        {
            Entities1[i].Component1.LifePoints = 247;
        }
    }
}

public class PlayerProcessor : Processor<Query<NameComponent, HealthComponent>>
{
    Random rand = new Random();

    public override void Update()
    {
        for (int i = 0; i < Entities1.Length; i++)
        {
            Console.WriteLine(Entities1[i].Component1.Name);
            var (name, health) = Entities1[i];
            name.Name = "Bob Kane";
            Console.WriteLine("has it changed ? : " + Entities1[i].Component1.Name);

        }
    }
}
