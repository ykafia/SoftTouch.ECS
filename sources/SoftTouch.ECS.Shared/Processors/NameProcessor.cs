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
    public class OtherNameProcessor : Processor<Query<NameComponent>>
    {
        public override void Update()
        {
            foreach (var arch in World.QueryArchetypes(Entities1.ID))
            {
                for (int i = 0; i < arch.Length; i++)
                {
                    arch.GetComponentSpan<NameComponent>()[i].Name = "Lilicia";
                }
            }
        }
    }
    public class IteratorNameProcessor : Processor<Query<NameComponent>>
    {
        public override void Update()
        {
            var entities = Entities1.CreateIterator();
            while(entities.Next())
            {
                entities.Set<NameComponent>(new NameComponent() {Name = "Jolyne"});

            }
        }
    }
}