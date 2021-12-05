# ECSharp

This project is a prototype for an ECS implementation with archetypal storage, heavily inspired by FLECS.

## Example

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