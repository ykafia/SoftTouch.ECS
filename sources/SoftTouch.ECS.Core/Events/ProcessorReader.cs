using CommunityToolkit.HighPerformance;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Events;

public class ProcessorReader<T>(EventList<T>? list, Processor processor)
    where T : struct, IEvent
{

    readonly EventList<T>? events = list;
    public Processor Processor { get; init; } = processor;
    public int ReadCount = 0;

    public Enumerator GetEnumerator() => new(events, this);

    public ref struct Enumerator
    {
        Span<T>.Enumerator enumerator;
        readonly ProcessorReader<T> reader;

        public Enumerator(EventList<T>? list, ProcessorReader<T> reader)
        {
            if (list != null && list.Count > reader.ReadCount)
                enumerator = list.events.AsSpan()[reader.ReadCount..].GetEnumerator();
            else
                enumerator = Span<T>.Empty.GetEnumerator();
            this.reader = reader;
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
