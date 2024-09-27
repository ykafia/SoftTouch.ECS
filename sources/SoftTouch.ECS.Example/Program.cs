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

static void BroadCastSomeData(EventWriter<ChangedAge> evw, Commands commands)
{
    evw.Broadcast(new() { Age = 12 });
    commands.Spawn(new TimeCount(TimeSpan.FromSeconds(2)));
    commands.Spawn(new NameComponent("John Doe"));
}

var app =
    new App()
    .AddProcessor<Startup, EventWriter<ChangedAge>, Commands>(BroadCastSomeData)
    .AddProcessor<Update, EventReader<ChangedAge>, Resource<AppTime>>(
        static (EventReader<ChangedAge> evw, Resource<AppTime> appTime) =>
        {
            bool any = false;
            foreach (var ev in evw.Receive())
            {
                Console.WriteLine($"Age has been changed to {ev.Age}");
                any = true;
            }
            if (any)
            {
                Console.WriteLine(appTime.Content.TotalElapsed);
                Console.WriteLine();
            }
        }
    )
    .AddProcessor<Update, EventWriter<ChangedAge>, Query<TimeCount>, Resource<AppTime>>(static (EventWriter<ChangedAge> ageChange, Query<TimeCount> tc, Resource<AppTime> time) =>
    {
        var elapsed = time.Content.Elapsed.TotalSeconds;
        foreach (var e in tc)
        {
            ref var t = ref e.Get<TimeCount>();
            t.Time += TimeSpan.FromSeconds(elapsed);
            if (t.Time > t.Delay)
            {
                ageChange.Broadcast(new() { Age = 13 });
                t.Time = TimeSpan.Zero;
            }
        }
    })
    .AddProcessor<Update, Query<NameComponent>, EventWriter<ChangedAge>>(
        static (Query<NameComponent> named, EventWriter<ChangedAge> ev) =>
    {

    });




app.Run();
