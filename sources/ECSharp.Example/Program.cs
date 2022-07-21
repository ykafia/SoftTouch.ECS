// See https://aka.ms/new-console-template for more information
using ECSharp.Arrays;
using ECSharp.Components;
using ECSharp.ComponentData;
using ECSharp;

var world = new World();

world
.CreateEntity()
.With(new NameComponent{Name = "Lola"});

Console.WriteLine("Hello, World!");
