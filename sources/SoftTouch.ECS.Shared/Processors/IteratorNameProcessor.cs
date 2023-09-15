//using SoftTouch.ECS.Processors;
//using SoftTouch.ECS.Querying;
//using SoftTouch.ECS.Shared.Components;

//namespace SoftTouch.ECS.Shared.Processors
//{
//    public class IteratorNameProcessor : Processor<Query<Read<NameComponent, HealthComponent>>>
//    {
//        public IteratorNameProcessor(World world) : base(world)
//        {
//        }

//        public override void Update()
//        {
//            foreach(var e in Query)
//            {
//                //var (name, health) = e;
//                e.Set(new NameComponent() {Name = "Jolyne"});

//            }
//        }
//    }
//}