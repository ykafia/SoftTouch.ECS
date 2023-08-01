// See https://aka.ms/new-console-template for more information
using SoftTouch.ECS.Shared.Components;
using SoftTouch.ECS;
using SoftTouch.ECS.Events;

namespace SoftTouch.ECS.Example
{
    // public class NameProcessor : Processor<Query<NameComponent>>
    // {
    //     public static EventKey<(string, long)> NewNameEvent = new EventKey<(string, long)>(eventName: "New Name");
    //     EventReceiver<(string, long)> NewNameReceiver = new EventReceiver<(string, long)>(NewNameEvent);
    //     public override async Task Execute()
    //     {
            
    //         while(World.IsRunning)
    //         {
    //             (string name, long id) = await NewNameReceiver.ReceiveAsync();
    //             World[id].Set(new NameComponent{Name = name});
    //         }
    //     }
    // }

    // public class ChangeName : Processor<Query<NameComponent>>
    // {
    //     public static EventKey<(string, long)> NewNameEvent = new EventKey<(string, long)>(eventName: "New Name");
    //     EventReceiver<(string, long)> NewNameReceiver = new EventReceiver<(string, long)>(NewNameEvent);

    //     public override void Update()
    //     {
    //         if(World.FrameCount % 15 == 0)
    //         {
    //             NameProcessor.NewNameEvent.Broadcast(("NameNumber" + World.FrameCount, 0));
    //         }
    //     }
    // }
    
}