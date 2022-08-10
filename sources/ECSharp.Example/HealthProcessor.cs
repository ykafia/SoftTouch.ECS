// See https://aka.ms/new-console-template for more information
using ECSharp.Components;
using ECSharp;

namespace ECSharp.Example
{
    public class HealthProcessor : Processor<QueryEntity<HealthComponent>>
    {

        public override void Update()
        {
            foreach (var arch in Query1)
                for (int i = 0; i < arch.Length; i++)
                {
                    // arch.GetComponentArray<HealthComponent>()[i].LifePoints = 150;
                }
        }
    }
}