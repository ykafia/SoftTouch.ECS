using SoftTouch.ECS.Shared.Components;
using System.Linq;

namespace SoftTouch.ECS.Shared.Processors
{
    public class NameProcessor : Processor<Query<NameComponent>>
    {
        public NameProcessor(){}

        public override void Update()
        {
            var length = Entities1.Length;
            for(int i = 0; i< length; i++)
            {
                Entities1[i].Component1.Name = "Batman";
            }
        }
    }
}