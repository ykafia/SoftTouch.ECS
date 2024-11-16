// See https://aka.ms/new-console-template for more information

using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Shared.Components;
using System.Security.Cryptography.X509Certificates;

namespace SoftTouch.ECS.Example;

public class StartupProcessor : Processor<Resource<WorldCommands>>
{
    public StartupProcessor() : base(null!)
    {

    }
    public override void Update()
    {
        Random rand = new Random();
        WorldCommands commands = Query;
        commands.Spawn((NameComponent)"John");
        commands.Spawn((NameComponent)"Jane");
        commands.Spawn((NameComponent)"Mary");
        // for (int i = 0; i < 2000; i++)
        // {
        //     commands.Spawn(i, new NameComponent($"john n°{i}"));
        // }
    }
}

public class WriteAge : Processor<Query<int, NoFilter>>
{
    public WriteAge() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            //Console.WriteLine($"There's a person that is {entity.Get<int>()} years old");
            var nb = entity.Get<int>();
            if (nb == 5)
                entity.Despawn();
        }
    }
}
public class SayBye : Processor<Query<NameComponent, Filter<Without<int>>>>
{
    public SayBye() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {

            if(entity.Get<NameComponent>().Name == "John")
            {
                Console.WriteLine($"Bye {entity.Get<NameComponent>().Name}.");
                entity.Despawn();
            }
            else if (entity.Get<NameComponent>().Name == "Mary")
            {
                entity.Add(3);
            }
        }
    }
}

public class SayHello : Processor<Query<NameComponent, NoFilter>>
{
    public SayHello() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            Console.WriteLine($"Hello {entity.Get<NameComponent>().Name}.");
        }
        Console.WriteLine();
    }
}
