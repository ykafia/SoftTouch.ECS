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
        for(int i = 0; i < 1000; i++)
        {
            commands.Spawn(rand.Next(1,100), new NameComponent($"john n°{i}"));
        }
    }
}

public class WriteAge : Processor<Query<Read<int>>>
{
    public WriteAge() : base(null!) { }
    public override void Update()
    {
        foreach(var entity in Query)
        {
            Console.WriteLine($"There's a person that is {entity.Get<int>()} years old");
        }
    }
}
public class SayHello : Processor<Query<Read<NameComponent>>>
{
    public SayHello() : base(null!) { }
    public override void Update()
    {
        foreach (var entity in Query)
        {
            Console.WriteLine($"Hello {entity.Get<NameComponent>().Name}!");
        }
    }
}
