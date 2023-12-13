// See https://aka.ms/new-console-template for more information


using SoftTouch.ECS;
using SoftTouch.ECS.Example;
using SoftTouch.ECS.Querying;
using SoftTouch.ECS.Storage;
using System.Diagnostics;


var myId = new ArchetypeID(typeof(int), typeof(float));
var myOtherId = new ArchetypeID(typeof(float), typeof(int));
var myOtherId2 = new ArchetypeID(typeof(float), typeof(int), typeof(long));


Console.WriteLine(myId == myOtherId);
Console.WriteLine($"{myId.GetHashCode()} - {myOtherId.GetHashCode()}");
Console.WriteLine(myId == myOtherId2);
Console.WriteLine(myOtherId == myOtherId2);
Console.WriteLine(myOtherId2.IsSupersetOf(myOtherId2));


HashSet<ArchetypeID> ids = [
    myId, myOtherId, myOtherId2
];
Console.WriteLine(ids.Count);
foreach(var id in ids)
{
    Console.WriteLine(string.Join(", ", id.Types.Select(x => x.FullName ?? "")));
}

// var app =
//     new App()
//     .AddStartupProcessor<StartupProcessor>()
//     .AddProcessor<SayHello>()
//     .AddProcessor<WriteAge>();
// var s = new Stopwatch();
// var fps = 0d;
// var min = double.PositiveInfinity;
// while (true)
// {
//     app.Update();
//     Console.Write($"\r{app.AppTime.FramePerSecond,6:F2}  ");
// }
