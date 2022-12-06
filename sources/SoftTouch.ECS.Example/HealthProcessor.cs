// See https://aka.ms/new-console-template for more information
using SoftTouch.ECS.Components;
using SoftTouch.ECS;

namespace SoftTouch.ECS.Example
{
    public class HealthProcessor : Processor<Query<HealthComponent>>
    {
        Random rand = new Random();

        public override void Update()
        {
            foreach(var e in query1.QueriedArchetypes)
            {
                
            }
        }
    }
}