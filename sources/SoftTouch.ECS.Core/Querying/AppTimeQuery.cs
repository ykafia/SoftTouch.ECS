using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Querying;

public interface ITime : IWorldQuery
{
}


public partial struct Time : IWorldQuery
{
    public World World { get; set; }
    public Processor CallingProcessor { get; init; }
    public readonly AppTime Content => World.Resources.Get<AppTime>();

    public readonly TimeSpan Elapsed => Content.Elapsed;
    public readonly TimeSpan WarpElapsed => Content.WarpElapsed;
    public readonly TimeSpan TotalElapsed => Content.TotalElapsed;
    public readonly long FrameCount => Content.FrameCount;

}