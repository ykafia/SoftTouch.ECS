using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class HealthProcessor : Processor<QueryEntity<HealthComponent>>
    {
        public override void Update()
        {
            Query1
            .AsParallel()
            .ForAll(
                x => {
                    for(int i =0; i< x.Length; i++)
                    {
                        var v = x.GetComponentArray<HealthComponent>()[i];
                        x.GetComponentArray<HealthComponent>()[i] = v with {LifePoints = v.LifePoints - 50};
                    }
                }
            );
        }
    }
}