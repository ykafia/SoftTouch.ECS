using SoftTouch.ECS.Components;
using System.Linq;

namespace SoftTouch.ECS.Processors
{
    public class NameProcessor : Processor<Query<NameComponent>>
    {
        public NameProcessor(World world)
        {
        }

        public override void Update()
        {
            // foreach((var e, var name) in query1)
            //     e.Set(name with {Name = "Lola"});
        }
    }
}