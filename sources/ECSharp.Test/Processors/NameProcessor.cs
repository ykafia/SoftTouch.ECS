using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class NameProcessor : Processor<QueryEntity<NameComponent>>
    {
        public override void Update()
        {
            Query1
            .AsParallel()
            .ForAll(
                x => {
                    for(int i =0; i< x.Length; i++)
                        x.GetComponentArray<NameComponent>()[i] = new NameComponent{Name = "Lola2"};
                }
            );
        }
    }
}