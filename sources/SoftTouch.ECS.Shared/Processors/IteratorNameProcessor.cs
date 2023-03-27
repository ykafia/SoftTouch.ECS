using SoftTouch.ECS.Shared.Components;

namespace SoftTouch.ECS.Shared.Processors
{
    public class IteratorNameProcessor : Processor<Query<NameComponent, HealthComponent>>
    {
        public override void Update()
        {
            foreach(var e in Entities1)
            {
                var (name, health) = e;
                e.Set(new NameComponent() {Name = "Jolyne"});

            }
        }
    }
}