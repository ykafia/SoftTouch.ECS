using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance;
using CommunityToolkit.HighPerformance.Buffers;

namespace SoftTouch.ECS.Arrays;


/// <summary>
/// An array of structs representing components
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("ComponentsArray<{typeof(T)}>[{Count}]")]
public class ComponentArray<T> : ComponentArray, ICollection<T>
    where T : struct
{
    readonly List<T> _owner = [];

    public Span<T> Span => CollectionsMarshal.AsSpan(_owner);

    public override int Count => _owner.Count;

    public override Type ComponentType => typeof(T);

    public bool IsReadOnly => throw new NotImplementedException();

    public T this[int index]
    {
        get => Span[index];
        set => Span[index] = value;
    }

    

    public Span<T>.Enumerator GetEnumerator() => Span.GetEnumerator();

    public void Add(T item) => _owner.Add(item);
    public void AddRange(List<T> items) => _owner.AddRange(items);
    public void AddRange(Span<T> items) => _owner.AddRange(items);
    public void AddRange(ComponentArray<T> items) => _owner.AddRange(items.Span);
    public bool Remove(T item) => _owner.Remove(item);
    public override bool RemoveAt(int index)
    {
        var result = index < Count && index > 0;
        _owner.RemoveAt(index);
        return result;
    }
    public bool RemoveAt(int index, out T item)
    {
        item = Span[index];
        return RemoveAt(index);
    }

    public override void MoveTo(int index, ComponentArray componentArray)
    {
        if (componentArray is ComponentArray<T> casted)
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

    public override void Clear()
    {
        _owner.Clear();
    }

    public override void CopyTo(ComponentArray componentArray)
    {
        if (componentArray is ComponentArray<T> other)
        {
            other._owner.Clear();
            other.AddRange(Span);
        }
    }

    public override ComponentArray Clone()
    {
        var clone = new ComponentArray<T>();
        CopyTo(clone);
        return clone;
    }

    public override bool TryAdd(ComponentBox item)
    {
        if (item is ComponentBox<T> cb)
        {
            Add(cb.Value);
            return true;
        }
        return false;
    }

    public override ComponentBox GetComponent(int idx)
    {
        return ComponentBox<T>.Create(Span[idx]);
    }

    public bool Contains(T item)
    {
        foreach (var e in Span)
            if (EqualityComparer<T>.Default.Equals(e, item))
                return true;
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        Span[arrayIndex..].CopyTo(array.AsSpan());
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return _owner.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _owner.GetEnumerator();
    }
}