// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;
using System.Diagnostics;


var app =
    new App()
    .AddStartupProcessor<StartupProcessor>();
// .AddProcessor<SayBye>()
// .AddProcessor<SayHello>()
// .AddProcessor<WriteAge>();

app.Update(false);
app.Update(false);
app.Update(false);

if (app.World[0].Location.Archetype.ID.Count != 1)
    throw new Exception();
