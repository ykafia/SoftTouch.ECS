// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;
using System.Diagnostics;

var e = new Entity(1,0);
var e1 = new Entity(1, 1);
var e2 = new Entity(2, 2);
var e2b = new Entity(2, 2);

Console.WriteLine(e2 == e2b);
Console.WriteLine($"{e2.GetHashCode()} {e2b.GetHashCode()}");

HashSet<Entity> hash = [
    e,e1, e2, e2b
];

Console.WriteLine(hash.Count);

// var app =
//     new App()
//     .AddStartupProcessor<StartupProcessor>()
//     .AddProcessor<SayHello>()
//     .AddProcessor<WriteAge>();
// var s = new Stopwatch();
// var fps = 0d;
// var min = double.PositiveInfinity;
// while (true)
// {
//     app.Update();
//     Console.Write($"\r{app.AppTime.FramePerSecond,6:F2}  ");
// }
