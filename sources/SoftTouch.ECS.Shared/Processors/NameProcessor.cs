using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Shared.Components;
using System.Linq;

namespace SoftTouch.ECS.Shared.Processors
{
    public class NameProcessor : Processor<Query<NameComponent, NoFilter>>
    {
        public NameProcessor(World world) : base(world)
        {
        }

        public override void Update()
        {
            //var length = Query.Length;
            //for(int i = 0; i< length; i++)
            //{
            //    //Entities1[i].Component1.Name = "Batman";
            //}
        }
    }
    public class OtherNameProcessor : Processor<Query<NameComponent, NoFilter>>
    {
        public OtherNameProcessor(World world) : base(world)
        {
        }

        public override void Update()
        {
            //foreach (var arch in World.QueryArchetypes(Query.ID))
            //{
            //    for (int i = 0; i < arch.Length; i++)
            //    {
            //        arch.SetComponent<NameComponent>(i, new("Lilicia"));
            //    }
            //}
        }
    }
}