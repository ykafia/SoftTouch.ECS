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
                // e.Get<HealthComponent>()health.Set(new HealthComponent{LifePoints = 157});
                e.Set(health with {LifePoints = 125});
            }
        }
    }
}