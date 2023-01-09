using SoftTouch.ECS.Components;
using System.Linq;

namespace SoftTouch.ECS.Processors
{
    public class HealthProcessor : Processor<Query<HealthComponent>>
    {
        public HealthProcessor(World world)
        {
        }

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
        public HealthProcessorE(World world)
        {
        }

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
        public HealthProcessorQ(World world)
        {
        }

        public override void Update()
        {
            // foreach (var arch in entities1)
            //     for (int i = 0; i < arch.Length; i++)
            //     {
            //         // arch[i].Set(new HealthComponent(){LifePoints = 125});
            //     }
        }
    }
    public class HealthProcessorRO : Processor<Query<ROHealthComponent>>
    {
        public HealthProcessorRO(World world)
        {
        }

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
        public HealthProcessorQRO(World world)
        {
        }

        public override void Update()
        {
            // foreach (var arch in query1.QueriedArchetypes)
            //     for (int i = 0; i < arch.Length; i++)
            //     {
            //         // arch[i].Set(new HealthComponent(){LifePoints = 125});
            //     }
        }
    }
    public class HealthProcessorQS : Processor<Query<HealthComponent>>
    {
        public HealthProcessorQS(World world)
        {
        }

        public override void Update()
        {
            // foreach (var arch in query1.QueriedArchetypes)
            // {
            //     for (int i = 0; i < arch.Length; i++) { }
            //     // arch.GetComponentSpan<HealthComponent>()[i] = new HealthComponent(){LifePoints = 125};
            // }
        }
    }
    public class HealthProcessorQROS : Processor<Query<ROHealthComponent>>
    {
        public HealthProcessorQROS(World world)
        {
        }

        public override void Update()
        {
            // foreach (var arch in query1.QueriedArchetypes)
            // {
            //     for (int i = 0; i < arch.Length; i++)
            //     {
            //         // var old = arch.GetComponentSpan<ROHealthComponent>()[i];
            //         // arch.GetComponentSpan<ROHealthComponent>()[i] = new(125, 0);
            //     }
            // }
        }
    }
}