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
        for (int i = 0; i < 2000; i++)
        {
            commands.Spawn(i, new NameComponent($"john n°{i}"));
        }
    }
}

public class WriteAge : Processor<Query<int>>
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
public class SayHello : Processor<Query<NameComponent,float>>
{
    public SayHello() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            //Console.WriteLine($"Hello {entity.Get<NameComponent>().Name}!");
            entity.Set<float>(entity.Get<NameComponent>().Name.Length);
        }
    }
}
