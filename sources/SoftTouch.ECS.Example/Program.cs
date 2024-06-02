// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Attributes;
using SoftTouch.ECS.Events;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Scheduling;
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS.Shared.Processors;
using SoftTouch.ECS.Storage;
using System.Diagnostics;


var app =
    new App()
    .AddProcessors<Update>(
        Processor.From(
            static (EventWriter<ChangedAge> evw, Query<double> d) => evw.Add(new() { Age = 12 })
        ),
        Processor.From(
            static (EventReader<ChangedAge> evw, Query<int, float> d, Query<NameComponent> namedEntities) =>
            {
                if (evw.Read().Count > 0)
                    Console.WriteLine(evw.Read()[0].Age);
            }
        )
    );



var x = 0;
// foreach(var t in p.RelatedEvents)
//     Console.WriteLine(t.FullName);