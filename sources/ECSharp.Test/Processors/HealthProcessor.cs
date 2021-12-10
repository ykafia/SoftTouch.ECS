using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class HealthProcessor : Processor<QueryEntity<HealthComponent>>
    {
        public override void Update()
        {
            GetQuery1()
            .AsParallel()
            .ForAll(
                x => {
                    for(int i =0; i< x.Length; i++)
                    {
                        var v = x.GetComponentArrayStruct<HealthComponent>()[i];
                        x.GetComponentArrayStruct<HealthComponent>()[i] = v with {LifePoints = v.LifePoints - 50};
                    }
                }
            );
        }
    }
}