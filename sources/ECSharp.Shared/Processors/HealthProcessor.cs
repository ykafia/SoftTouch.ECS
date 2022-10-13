using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class HealthProcessor : Processor<Query<HealthComponent>>
    {


        public override void Update()
        {
            // foreach ((var e, var health) in query1)
            // {
            //     // e.Get<HealthComponent>()health.Set(new HealthComponent{LifePoints = 157});
            //     // e.Set(health with {LifePoints = 125});
            // }
        }
    }
    public class HealthProcessorE : Processor<Query<HealthComponent>>
    {
        public override void Update()
        {
            // foreach ((var e, var health) in query1)
            // {
            //     // e.Get<HealthComponent>()health.Set(new HealthComponent{LifePoints = 157});
            //     // e.Set(health with {LifePoints = 125});
            // }
        }
    }
    public class HealthProcessorQ : Processor<Query<HealthComponent>>
    {
        public override void Update()
        {
            foreach (var arch in query1.QueriedArchetypes)
                for (int i = 0; i < arch.Length; i++)
                {
                    // arch[i].Set(new HealthComponent(){LifePoints = 125});
                }
        }
    }
    public class HealthProcessorRO : Processor<Query<ROHealthComponent>>
    {
        public override void Update()
        {
            // foreach ((var e, var _) in query1)
            // {
            //     // e.Set(new HealthComponent(){LifePoints = 125});
            // }
        }
    }
    public class HealthProcessorQRO : Processor<Query<ROHealthComponent>>
    {
        public override void Update()
        {
            foreach (var arch in query1.QueriedArchetypes)
                for (int i = 0; i < arch.Length; i++)
                {
                    // arch[i].Set(new HealthComponent(){LifePoints = 125});
                }
        }
    }
    public class HealthProcessorQS : Processor<Query<HealthComponent>>
    {
        public override void Update()
        {
            foreach (var arch in query1.QueriedArchetypes)
            {
                for (int i = 0; i < arch.Length; i++) { }
                // arch.GetComponentSpan<HealthComponent>()[i] = new HealthComponent(){LifePoints = 125};
            }
        }
    }
    public class HealthProcessorQROS : Processor<Query<ROHealthComponent>>
    {
        public override void Update()
        {
            foreach (var arch in query1.QueriedArchetypes)
            {
                for (int i = 0; i < arch.Length; i++)
                {
                    // var old = arch.GetComponentSpan<ROHealthComponent>()[i];
                    // arch.GetComponentSpan<ROHealthComponent>()[i] = new(125, 0);
                }
            }
        }
    }
}