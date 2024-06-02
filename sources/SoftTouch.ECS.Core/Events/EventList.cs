using System.Collections;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;



public abstract class EventList;

public class EventList<T> : EventList, IList<T> where T : struct
{
    readonly List<T> events = [];

    public T this[int index] { get => events[index]; set => events[index] = value; }

    public int Count => events.Count;

    public bool IsReadOnly => false;

    public IReadOnlyList<T> AsReadOnly() => events.AsReadOnly();

    public void Add(T item)
        => events.Add(item);

    public void Clear()
        => events.Clear();

    public bool Contains(T item)
        => events.Contains(item);

    public void CopyTo(T[] array, int arrayIndex)
        => events.CopyTo(array, arrayIndex);

    public IEnumerator<T> GetEnumerator()
        => events.GetEnumerator();

    public int IndexOf(T item)
        => events.IndexOf(item);

    public void Insert(int index, T item)
        => events.Insert(index, item);

    public bool Remove(T item)
        => events.Remove(item);

    public void RemoveAt(int index)
        => events.RemoveAt(index);

    IEnumerator IEnumerable.GetEnumerator()
        => events.GetEnumerator();
}