// See https://aka.ms/new-console-template for more information
using ECSharp.Arrays;
using ECSharp.Components;
using ECSharp.ComponentData;
using ECSharp;
using ECSharp.Example;

var world = new World();


world
    .CreateEntity()
    .With(new NameComponent{Name = "Bonobo"})
    .With(new HealthComponent{LifePoints = 100});
world
    .CreateEntity()
    .With(new NameComponent{Name = "Bonobo2"})
    .With(new HealthComponent{LifePoints = 100});

world.Add<HealthProcessor>();
// world.Add<NameProcessor>();
// world.Add<ChangeName>();

world.Run(5);