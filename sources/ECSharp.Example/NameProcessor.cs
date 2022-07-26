// See https://aka.ms/new-console-template for more information
using ECSharp.Components;
using ECSharp;
using ECSharp.Events;

namespace ECSharp.Example
{
    public class NameProcessor : ProcessorAsync<QueryEntity<NameComponent>>
    {
        public static EventKey<(string, long)> NewNameEvent = new EventKey<(string, long)>(eventName: "New Name");
        EventReceiver<(string, long)> NewNameReceiver = new EventReceiver<(string, long)>(NewNameEvent);
        public override async Task Execute()
        {
            
            while(World.IsRunning)
            {
                (string name, long id) = await NewNameReceiver.ReceiveAsync();
                World[id].Set<NameComponent>(new NameComponent{Name = name});
            }
        }
    }

    public class ChangeName : Processor<QueryEntity<NameComponent>>
    {
        public static EventKey<(string, long)> NewNameEvent = new EventKey<(string, long)>(eventName: "New Name");
        EventReceiver<(string, long)> NewNameReceiver = new EventReceiver<(string, long)>(NewNameEvent);
        public override void Update()
        {
            if(World.FrameCount % 15 == 0)
            {
                NameProcessor.NewNameEvent.Broadcast(("NameNumber" + World.FrameCount, 0));
            }

        }
    }
    
}