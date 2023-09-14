// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS.Querying;



//public 



//using SoftTouch.ECS.Arrays;
//using SoftTouch.ECS.Shared.Components;
//using SoftTouch.ECS.ComponentData;
//using SoftTouch.ECS;
//using SoftTouch.ECS.Example;
//using System.Collections.Generic;
//using System.Linq;

//var world = new World();


//foreach()

// world
//     .CreateEntity()
//     .With(new NameComponent{Name = "Bonobo"})
//     .With(new HealthComponent{LifePoints = 100});
// world
//     .CreateEntity()
//     .With(new NameComponent{Name = "Bonobo2"})
//     .With(new HealthComponent{LifePoints = 100});

// world.Add<HealthProcessor>();
// world.Add<NameProcessor>();
// world.Add<ChangeName>();

// var x = new SortedList<ArchetypeID,string>();

// x.Add(new(typeof(HealthComponent)), "first");
// x.Add(new(typeof(NameComponent)), "second");

//world.Commands.Spawn()
//    .With<HealthComponent>();
//world.Commands.Spawn(new NameComponent(){Name = "Martha"})
//    .With<HealthComponent>();

//world.AddProcessor<PlayerProcessor>();
//world.Start();
//world.Update();

//Console.WriteLine(world[1].Get<NameComponent>().Name);
// world.CreateEntity()
//     .With<NameComponent>()
//     .With<HealthComponent>()
//     .With<ModelComponent>();
// world.CreateEntity()
//     .With<int>()
//     .With<HealthComponent>()
//     .With<uint>();
// world.CreateEntity()
//     .With<int>()
//     .With<HealthComponent>();
// world.CreateEntity()
//     .With<float>()
//     .With<HealthComponent>()
//     .With<uint>();
// world.CreateEntity()
//     .With<float>()
//     .With<HealthComponent>()
//     .With<double>();
// world.CreateEntity()
//     .With<float>()
//     .With<HealthComponent>();

// world.Run(5);