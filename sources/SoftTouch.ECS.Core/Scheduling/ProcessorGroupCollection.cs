using CommunityToolkit.HighPerformance.Buffers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public class ProcessorGroupCollection : IDisposable, ICollection<ProcessorGroup>
{
    MemoryOwner<ProcessorGroup> owner;
    public int Length { get; private set; }
    public Memory<ProcessorGroup> Memory => owner.Memory[..Length];
    public Span<ProcessorGroup> Span => owner.Span[..Length];

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public ProcessorGroup this[int index] => Span[index];


    public ProcessorGroupCollection()
    {
        owner = MemoryOwner<ProcessorGroup>.Allocate(4, AllocationMode.Clear);
        Length = 0;
    }

    void Expand(int size)
    {
        var newSize = Length + size;
        if (newSize > owner.Length)
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

    public void Dispose()
    {
        owner.Dispose();
    }

    public void Clear()
    {
        Span.Clear();
        Length = 0;
    }

    public bool Contains(ProcessorGroup item)
    {
        foreach (var e in Span)
            if (EqualityComparer<ProcessorGroup>.Default.Equals(e, item))
                return true;
        return false;
    }

    public void CopyTo(ProcessorGroup[] array, int arrayIndex)
    {
        Span[arrayIndex..].CopyTo(array.AsSpan());
    }

    public bool Remove(ProcessorGroup item)
    {
        var idx = 0;
        foreach (var e in Span)
        {
            if (EqualityComparer<ProcessorGroup>.Default.Equals(e, item))
            {
                Span[(idx + 1)..].CopyTo(Span[idx..]);
                Length -= 1;
                return true;
            }
            idx += 1;
        }
        return false;
    }

    IEnumerator<ProcessorGroup> IEnumerable<ProcessorGroup>.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
