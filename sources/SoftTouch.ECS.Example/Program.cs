// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;

var app =
    new App()
    .AddStartupProcessor<StartupProcessor>()
    .AddProcessor<SayHello>()
    .AddProcessor<WriteAge>();
// foreach(var e in )
app.Update();
var x = 0;
