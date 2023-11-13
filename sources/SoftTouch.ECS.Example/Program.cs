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
    app.Update();
    Console.Write($"\r{app.AppTime.FramePerSecond,6:F2}  ");
}
