```csharp
public class HealthProcessor : Processor<Query<HealthComponent, NameComponent>>
{
    public override void Update()
    {
        foreach((var health, var name) in Query1)
        {

        }
    }
}

```