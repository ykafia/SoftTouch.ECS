# WONK-ECS

It's a wonky Entity-Component-System library using Archetypal storage.
It's heavily inspired by FLECS.

## UseCase

```csharp
var world = new World();
world
    .CreateEntity()
    .With(new NameComponent{Name = "John"})
    .With(new HealthComponent{LifePoints = 100, Shield = 100})
    .Build();
world
    .CreateEntity()
    .With(new NameComponent{Name = "Lola"})
    .Build();
world
    .CreateEntity()
    .With(new NameComponent{Name = "Toto"})
    .Build();

world[0].Remove<HealthComponent>();
world.Processors.Add(new NameProcessor());
world.Update();
```

## FAQ

1. Why is it wonky ?
    Well it's not benchmarked, the api is not battle tested and i don't trust my current knowledge of C# enough to second guess it's performance.
