using ECSharp.Components;
using System.Linq;
using System.Numerics;

namespace ECSharp.Processors
{
    public class ModelProcessor : Processor<QueryEntity<ModelComponent>>
    {
        public override void Update()
        {
            GetQuery1()
            .AsParallel()
            .ForAll(
                x => {
                    for(int i =0; i< x.Length; i++)
                    {
                        x.GetComponentArray<ModelComponent>()[i].Buffer.Add(Vector3.One);
                    }
                }
            );
        }
    }
}