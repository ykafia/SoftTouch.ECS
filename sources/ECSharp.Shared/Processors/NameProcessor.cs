using ECSharp.Components;
using System.Linq;

namespace ECSharp.Processors
{
    public class NameProcessor : Processor<Query<NameComponent>>
    {
        public override void Update()
        {
            foreach((var e, var name) in query1)
                e.Set(name with {Name = "Lola"});
        }
    }
}