using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;

public abstract class ComponentArray
{
    public abstract Type ComponentType { get; }
    public int Count { get; protected set; }
    public abstract bool TryAdd<TOther>(TOther item) where TOther : struct, IEquatable<TOther>;
    public abstract bool TryRemove<TOther>(TOther item) where TOther : struct, IEquatable<TOther>;
    public abstract bool TryRemoveAt<TOther>(int index, out TOther item) where TOther : struct, IEquatable<TOther>;

    public abstract ComponentArray Create();

    public abstract void MoveTo(int index, ComponentArray componentArray);
}

public class ComponentArray<T> : ComponentArray
    where T : struct
{
    MemoryOwner<T> _owner;
    public Span<T> Span => _owner.Span[..Count];

    public override Type ComponentType => typeof(T);

    public T this[int index]
    {
        get => Span[index];
        set => Span[index] = value;
    }

    public ComponentArray()
    {
        _owner = MemoryOwner<T>.Allocate(8, AllocationMode.Clear);
        Count = 0;
    }
    public ComponentArray(int size)
    {
        _owner = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)size), AllocationMode.Clear);
        Count = 0;
    }

    public Span<T>.Enumerator GetEnumerator() => Span.GetEnumerator();


    void Expand(int size)
    {
        if (_owner.Length < Count + size)
        {
            var nbuff = MemoryOwner<T>.Allocate((int)BitOperations.RoundUpToPowerOf2((uint)(Count + size)), AllocationMode.Clear);
            _owner.Span.CopyTo(nbuff.Span);
            _owner = nbuff;
        }
    }

    public void Add(T item)
    {
        Expand(1);
        _owner.Span[Count] = item;
        Count += 1;
    }
    public void AddRange(List<T> items)
    {
        Expand(items.Count);
        CollectionsMarshal.AsSpan(items).CopyTo(_owner.Span[Count..]);
        Count += items.Count;
    }
    public void AddRange(ComponentArray<T> items)
    {
        Expand(items.Count);
        items.Span.CopyTo(_owner.Span[Count..]);
        Count += items.Count;
    }
    public bool Remove(T item)
    {
        int i = 0;
        foreach (var e in _owner.Span)
        {
            i += 1;
            if (e.Equals(item))
                return RemoveAt(i);
        }
        return false;
    }
    public bool RemoveAt(int index)
    {
        Span[(index + 1)..].CopyTo(Span[index..]);
        Count -= 1;
        return true;
    }
    public bool RemoveAt(int index, out T item)
    {
        item = Span[index];
        return RemoveAt(index);
    }

    public override void MoveTo(int index, ComponentArray componentArray)
    {
        if(componentArray is ComponentArray<T> casted)
        {
            RemoveAt(index, out var item);
            casted.Add(item);
        }
    }

    public override bool TryAdd<TOther>(TOther item)
    {
        if (typeof(TOther) == typeof(T))
        {
            Add(Unsafe.As<TOther, T>(ref item));
            return true;
        }
        return false;
    }
    public override bool TryRemove<TOther>(TOther item)
    {
        if (typeof(TOther) == typeof(T))
        {
            Remove(Unsafe.As<TOther, T>(ref item));
            return true;
        }
        return false;
    }

    public override bool TryRemoveAt<TOther>(int index, out TOther item)
    {
        if (typeof(TOther) == typeof(T))
        {
            var result = Span[index];
            item = Unsafe.As<T, TOther>(ref result);
            RemoveAt(index);
            return true;
        }
        item = default;
        return false;
    }

    internal ComponentArray<T> With(T item)
    {
        Add(item);
        return this;
    }

    public override ComponentArray Create() => new ComponentArray<T>();

}