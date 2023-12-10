using System.Numerics;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;


public struct ReusableList<T>(int capacity = 4)
{
    MemoryOwner<T> _array = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)capacity), AllocationMode.Clear);
    public int Length { get; private set; }

    public readonly Span<T> Span => _array.Span[..Length];
    public readonly Memory<T> Memory => _array.Memory[..Length];

    void Expand(int size)
    {
        var newSize = Length + size;
        if(newSize > _array.Length)
        {
            var newArray = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)newSize), AllocationMode.Clear);
            _array.Span.CopyTo(newArray.Span);
            _array.Dispose();
            _array = newArray;
        }
    }

    public void Add(T item)
    {
        Expand(1);
        Span[Length] = item;
        Length+=1;
    }

    public readonly void Dispose()
    {
        _array.Dispose();
    }
}
