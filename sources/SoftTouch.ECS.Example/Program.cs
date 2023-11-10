// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;
using System.Diagnostics;

var app =
    new App()
    .AddStartupProcessor<StartupProcessor>()
    .AddProcessor<SayHello>()
    .AddProcessor<WriteAge>();
var s = new Stopwatch();
var fps = 0d;
var min = double.PositiveInfinity;
while (true)
{
    s.Restart();
    app.Update();
    s.Stop();
    fps  = 1/s.Elapsed.TotalSeconds;
    Console.Write($"\r{fps, 0:00000:F2}fps min : {min:0.##}          ");
}
