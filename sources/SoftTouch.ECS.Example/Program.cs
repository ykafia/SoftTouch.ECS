// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;
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

static void Machin(EventWriter<ChangedAge> eventList, Query<double> e1){}
static void Machin2(EventWriter<ChangedAge> eventList, Query<int, float> e1, Query<NameComponent> namedEntities){}

var app =
    new App();
app
    .AddProcessors<Main>(
        Processor.From<EventWriter<ChangedAge>, Query<double>>(Machin),
        Processor.From<EventWriter<ChangedAge>, Query<int, float>, Query<NameComponent>>(Machin2)
    );

var x = 0;
// foreach(var t in p.RelatedEvents)
//     Console.WriteLine(t.FullName);