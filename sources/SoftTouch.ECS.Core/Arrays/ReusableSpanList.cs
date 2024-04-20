using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;

public readonly ref struct ReusableSpanList
{
    public static ReusableSpanList<T> Create<T>(ReadOnlySpan<T> items) 
        => new(items);
}


[CollectionBuilder(typeof(ReusableSpanList), "Create")]
public readonly ref struct ReusableSpanList<T>
{
    SpanOwner<T> Owner { get; init;}
    public readonly Span<T> Span => Owner.Span;
    public int Count { get; init; }

    public ReusableSpanList()
    {
        Owner = SpanOwner<T>.Allocate(8, AllocationMode.Clear);
    }

    public ReusableSpanList(Span<T> span) : this()
    {
        var length = span.Length / 8 * 8 + 1;
        Owner = SpanOwner<T>.Allocate(length, AllocationMode.Clear);
        Count = span.Length;
    }
    public ReusableSpanList(ReadOnlySpan<T> span) : this()
    {
        var length = span.Length / 8 * 8 + 1;
        Owner = SpanOwner<T>.Allocate(length, AllocationMode.Clear);
        Count = span.Length;
    }

    /// <summary>
    /// Add an element and returns a new Span
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public readonly ReusableSpanList<T> Add(T item)
    {
        var size = Count == Owner.Span.Length ? Count + 8 : Count;
        var n = SpanOwner<T>.Allocate(size, AllocationMode.Clear);
        Span.CopyTo(n.Span);
        n.Span[Count] = item;
        Dispose();
        return new()
        {
            Owner = n,
            Count = Count + 1
        };
    }

    public readonly void Clear()
        => Span.Clear();

    public readonly bool Contains(T item)
    {
        foreach(var e in Span)
        {
            if(EqualityComparer<T>.Default.Equals(item, e))
                return true;
        }
        return false;
    }

    public readonly Span<T>.Enumerator GetEnumerator() => Owner.Span.GetEnumerator();

    /// <summary>
    /// Removes one element and returns a new ReusableSpanList. Current one is disposed
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public readonly ReusableSpanList<T> Remove(T item)
    {
        for(int i = 0; i < Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(item, Span[i]))
            {
                Span[(i+1)..].CopyTo(Span[i..]);
                var n = SpanOwner<T>.Allocate(Owner.Length, AllocationMode.Clear);
                Span.CopyTo(n.Span);
                Dispose();
                return new()
                {
                    Owner = n,
                    Count = Count - 1
                };
            }
        }
        return this;
    }

    public readonly void Dispose()
        => Owner.Dispose();


    public static implicit operator ReusableSpanList<T>(Span<T> span) => new(span);
}


