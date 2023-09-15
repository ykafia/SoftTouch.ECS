using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Shared.Components;
using System.Linq;

namespace SoftTouch.ECS.Shared.Processors
{
    public class HealthProcessor : Processor<Query<Read<HealthComponent>>>
    {
        public HealthProcessor(World world) : base(world)
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
    public class HealthProcessorE : Processor<Query<Read<HealthComponent>>>
    {
        public HealthProcessorE(World world) : base(world)
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
    public class HealthProcessorQ : Processor<Query<Read<HealthComponent>>>
    {
        public HealthProcessorQ(World world) : base(world)
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
    public class HealthProcessorRO : Processor<Query<Read<ROHealthComponent>>>
    {
        public HealthProcessorRO(World world) : base(world)
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
    public class HealthProcessorQRO : Processor<Query<Read<ROHealthComponent>>>
    {
        public HealthProcessorQRO(World world) : base(world)
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
    public class HealthProcessorQS : Processor<Query<Read<HealthComponent>>>
    {
        public HealthProcessorQS(World world) : base(world)
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
    public class HealthProcessorQROS : Processor<Query<Read<ROHealthComponent>>>
    {
        public HealthProcessorQROS(World world) : base(world)
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