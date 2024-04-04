using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public struct MergeStage<T> : IDisposable
{
    public ReusableList<Processor> Processors { get; set; }
    public readonly void Dispose() => Processors.Dispose();

}