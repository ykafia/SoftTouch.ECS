// See https://aka.ms/new-console-template for more information
using ECSharp.Components;
using ECSharp;

namespace ECSharp.Example
{
    public class HealthProcessor : Processor<Query<HealthComponent>>
    {
        Random rand = new Random();

        public override void Update()
        {
            foreach(var e in query1.QueriedArchetypes)
            {
                
            }
        }
    }
}