using System.Collections;
using CommunityToolkit.HighPerformance;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;



public abstract class EventList
{
    public abstract void UpdateEvents();
}

public struct ProcessorReader<T>(EventList<T>? list, Processor processor)
    where T : struct, IEvent
{

    readonly EventList<T>? events = list;
    public Processor Processor { get; init; } = processor;
    public int ReadCount = 0;

    public readonly Enumerator GetEnumerator() => new(events, Processor, ReadCount);

    public ref struct Enumerator
    {
        Span<T>.Enumerator enumerator;
        ref ProcessorReader<T> reader;

        public Enumerator(EventList<T>? list, Processor processor, int readCount)
        {
            if (list != null)
            {
                if (readCount >= list.Count)
                    enumerator = Span<T>.Empty.GetEnumerator();
                else
                {
                    enumerator = list.events.AsSpan()[readCount..].GetEnumerator();
                    reader = ref list.GetOrSubscribe(processor);
                }
            }
            else
                enumerator = Span<T>.Empty.GetEnumerator();
        }

        public T Current => enumerator.Current;

        public bool MoveNext()
        {
            if (enumerator.MoveNext())
            {
                reader.ReadCount += 1;
                return true;
            }
            return false;
        }
    }
}

public class EventList<T> : EventList, IList<T> where T : struct, IEvent
{
    readonly internal List<T> events = [];
    readonly internal List<ProcessorReader<T>> readers = [];

    public T this[int index] { get => events[index]; set => events[index] = value; }

    public int Count => events.Count;

    public bool IsReadOnly => false;

    public IReadOnlyList<T> AsReadOnly() => events.AsReadOnly();

    public ref ProcessorReader<T> GetOrSubscribe(Processor processor)
    {
        foreach (ref var p in readers.AsSpan())
            if (p.Processor == processor)
                return ref p;
        readers.Add(new(this, processor));
        return ref readers.AsSpan()[^1];
    }
    public bool Unsubscribe(Processor processor)
    {
        foreach (var p in readers)
            if (p.Processor == processor)
            {
                readers.Remove(p);
                return true;
            }
        return false;
    }

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

    public override void UpdateEvents()
    {
        var originalSize = events.Count;
        var size = 0;
        for (int i = originalSize - 1; i >= 0; i -= 1)
        {
            ref var e = ref events.AsSpan()[i];
            if (e.FrameAge >= 0 && e.FrameAge < 2)
            {
                e.FrameAge += 1;
                size += 1;
            }
            else
                events.RemoveAt(i);
        }
        foreach (ref var r in readers.AsSpan())
            r.ReadCount = Math.Max(0, r.ReadCount - originalSize + size);
    }
}