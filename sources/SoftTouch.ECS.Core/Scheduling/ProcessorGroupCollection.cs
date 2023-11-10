using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public class ProcessorGroupCollection
{
    MemoryOwner<ProcessorGroup> owner;
    public int Length { get; private set; }
    public Memory<ProcessorGroup> Memory => owner.Memory[..Length];
    public Span<ProcessorGroup> Span => owner.Span[..Length];


    public ProcessorGroup this[int index] => Span[index];


    public ProcessorGroupCollection()
    {
        owner = MemoryOwner<ProcessorGroup>.Allocate(4, AllocationMode.Clear);
        Length = 0;
    }

    void Expand(int size)
    {
        var newSize = Length + size;
        if(newSize > owner.Length)
        {
            var tmp = MemoryOwner<ProcessorGroup>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)newSize), AllocationMode.Clear);
            Span.CopyTo(tmp.Span);
            owner.Dispose();
            owner = tmp;
        }
    }

    public void Add(ProcessorGroup g)
    {
        Expand(1);
        Length += 1;
        Span[Length - 1] = g;
    }

    public Span<ProcessorGroup>.Enumerator GetEnumerator() => Span.GetEnumerator();
}
