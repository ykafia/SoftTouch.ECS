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
        WorldCommands commands = Query;
        commands.Spawn(1, new NameComponent("toto"));
        commands.Spawn(8, new NameComponent("john"));
        commands.Spawn(12, new NameComponent("jane"));
    }
}

public class MyProcessor : Processor<Query<Read<int>>>
{
    public MyProcessor() : base(null!) { }
    public override void Update()
    {
        foreach(var entity in Query)
        {
            Console.WriteLine($"Person with age {entity.Get<int>()} has for name {entity.Get<NameComponent>().Name}");
        }
    }
}