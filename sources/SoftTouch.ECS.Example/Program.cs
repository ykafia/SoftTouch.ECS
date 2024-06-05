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
    .AddProcessor<Startup, EventWriter<ChangedAge>, Commands>(
        static (EventWriter<ChangedAge> evw, Commands commands) =>
        {
            evw.Broadcast(new() { Age = 12 });
            commands.Spawn(new TimeCount(TimeSpan.FromSeconds(2)));
        })
    .AddProcessor<Update, EventReader<ChangedAge>>(
        static (EventReader<ChangedAge> evw) =>
        {
            foreach (var ev in evw.Receive())
                Console.WriteLine($"Age has been changed to {ev.Age}");
        }
    )
    .AddProcessor<Update, EventWriter<ChangedAge>, Query<TimeCount>, Resource<AppTime>>(static (EventWriter<ChangedAge> ageChange, Query<TimeCount> tc, Resource<AppTime> time) =>
    {
        var elapsed = time.Content.Elapsed.TotalSeconds;
        foreach(var e in tc)
        {
            ref var t = ref e.Get<TimeCount>();
            t.Time += TimeSpan.FromSeconds(elapsed);
            if(t.Time > t.Delay)
            {
                ageChange.Broadcast(new() { Age = 13 });
                t.Time = TimeSpan.Zero;
            }
        }
    });




app.Run(true);
