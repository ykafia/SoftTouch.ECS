using System.Runtime.CompilerServices;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;

namespace SoftTouch.ECS.Example.Rlib;


public class ExtractComponent<T>(World world) : Processor<Query<T, NoFilter>, Commands>(world)
    where T : struct
{

    public override void Update()
    {
        
    }
}