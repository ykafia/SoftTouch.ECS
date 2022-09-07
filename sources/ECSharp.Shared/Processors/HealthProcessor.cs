using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class HealthProcessor : Processor<Query<HealthComponent>>
    {
        public override void Update()
        {
            foreach((var e, var health) in query1)
            {
                health.Set(new HealthComponent{LifePoints = 157});
            }
        }
    }
}