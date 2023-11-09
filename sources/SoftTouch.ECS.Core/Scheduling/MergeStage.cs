using CommunityToolkit.HighPerformance.Buffers;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public struct MergeStage : IDisposable
{
    public string Name { get; set; }
    public MemoryOwner<Processor> Processors { get; set; }
    public readonly void Dispose() => Processors.Dispose();

}