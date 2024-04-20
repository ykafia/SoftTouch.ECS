// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Shared.Processors;
using SoftTouch.ECS.Storage;
using System.Diagnostics;


// var app =
//     new App()
//     .AddStartupProcessor<StartupProcessor>()
//     .AddProcessor<SayBye>()
//     .AddProcessor<SayHello>()
//     .AddProcessor<WriteAge>();

// app.Run();

static void Machin(Resource<EventList> eventList, Query<int, float> e1, Query<NameComponent> namedEntities)
{

}
var p = Processor.From<Resource<EventList>, Query<int, float>, Query<NameComponent>>(Machin);
foreach(var t in p.RelatedTypes)
    Console.WriteLine(t.FullName);