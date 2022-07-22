// See https://aka.ms/new-console-template for more information
using ECSharp.Arrays;
using ECSharp.Components;
using ECSharp.ComponentData;
using ECSharp;
using ECSharp.Example;

var world = new World();

for (int i = 0; i < 10000; i++)
    world
    .CreateEntity()
    .With(new HealthComponent{LifePoints = 100});

world.Add<HealthProcessor>();

world.Update();

Console.WriteLine("Hello, World!");
