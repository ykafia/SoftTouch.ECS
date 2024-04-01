using System.Collections;
using System.Numerics;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;


public class ReusableList<T> : IDisposable, ICollection<T>
{
    protected MemoryOwner<T> _array = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2(4), AllocationMode.Clear);
    public int Length { get; protected set; }

    public Span<T> Span => _array.Span[..Length];
    public Memory<T> Memory => _array.Memory[..Length];

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    protected void Expand(int size)
    {
        var newSize = Length + size;
        if (newSize > _array.Length)
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
        _array.Span[Length] = item;
        Length += 1;
    }

    public void Dispose()
    {
        _array.Dispose();
    }

    public void Clear()
    {
        Span.Clear();
        Length = 0;
    }

    public bool Contains(T item)
    {
        foreach(var e in Span)
            if(EqualityComparer<T>.Default.Equals(e,item))
                return true;
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        Span[arrayIndex..].CopyTo(array.AsSpan());
    }

    public bool Remove(T item)
    {
        var idx = -1;
        var tidx = 0;
        foreach(var e in Span)
        {
            if(EqualityComparer<T>.Default.Equals(e,item))
            {
                idx = tidx; 
                break;
            }
            tidx +=1;
        }
        if(idx == -1)
            return false;
        Span[(idx+1)..].CopyTo(Span[idx..]);
        Length -= 1;
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
