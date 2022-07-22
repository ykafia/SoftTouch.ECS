using System.Runtime.InteropServices;
using ECSharp.Components;

namespace ECSharp.Processors;

public class Processor1 : Processor<QueryEntity<HealthComponent>>
{
    public override void Update()
    {
        foreach(var arch in Query1)
            for (int i = 0; i < arch.Length; i++)
            {
                arch.GetComponentArray<HealthComponent>()[i] = new HealthComponent{LifePoints = 100, Shield = 100};
            }
    }
}
public class Processor2 : Processor<QueryEntity<HealthComponent>>
{
    public override void Update()
    {
        foreach(var arch in Query1)
            for (int i = 0; i < arch.Length; i++)
            {
                arch.GetEntityComponent<HealthComponent>(i, out var c);
                c.LifePoints = 100;
            }
    }
}
public class Processor3 : Processor<QueryEntity<NameComponent, HealthComponent>>
{
    public override void Update()
    {
        foreach(var arch in Query1)
            for (int i = 0; i < arch.Length; i++)
            {
                arch.GetComponentArray<HealthComponent>().Span[i].LifePoints = 100;
            }
    }
}