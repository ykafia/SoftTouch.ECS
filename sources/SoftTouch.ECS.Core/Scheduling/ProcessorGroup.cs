using System.Text;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.States;

namespace SoftTouch.ECS.Scheduling;


public class Group(WorldStates? states = null, StateEvent? stateEvent = null)
{
    HashSet<Type> RelatedEvents = [];
    HashSet<Type> RelatedTypes = [];
    List<Processor> Processors = [];
    List<Processor> SingleShots = [];
    WorldStates States = states!;
    StateEvent? stateEvent = stateEvent;

    public int Count => Processors.Count;

    internal Group With<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach (var t in p.RelatedTypes)
            RelatedTypes.Add(t);
        foreach (var t in p.RelatedEvents)
            RelatedEvents.Add(t);
        Processors.Add(p);
        return this;
    }

    /// <summary>
    /// Tries to add a processor in the group, if the processor has types related to the group then it can be added else it goes in another group.
    /// Additionally, it checks if the queries are Event readers and matches specific rules
    /// </summary>
    /// <typeparam name="TProcessor"></typeparam>
    /// <param name="p">Processor to add to the group</param>
    /// <returns>True if the processor has been added to the group, false if not.</returns>
    public bool TryAdd<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        var related = false;
        foreach (var t in p.RelatedEvents)
            if (RelatedEvents.Contains(t))
                return false;
        if (p.RelatedTypes.Count == 0 && RelatedTypes.Count == 0)
        {
            Processors.Add(p);
            return true;
        }
        else if (p.RelatedTypes.Count > 0 && RelatedTypes.Count > 0)
        {
            foreach (var t in p.RelatedTypes)
            {
                if (RelatedTypes.Contains(t))
                {
                    related = true;
                    Processors.Add(p);
                    foreach (var a in p.RelatedTypes)
                        RelatedTypes.Add(a);
                    foreach (var a in p.RelatedEvents)
                        RelatedEvents.Add(a);
                    break;
                }
            }
            return related;
        }
        else
            return false;
    }

    public bool TryRemove<TProcessor>(TProcessor processor)
        where TProcessor : Processor
    {
        if (!Processors.Contains(processor))
            return false;
        else
        {
            Processors.Remove(processor);
            foreach (var t in processor.RelatedTypes)
            {
                bool canBeDeleted = true;
                foreach (var p in Processors)
                {
                    if (p.RelatedTypes.Contains(t))
                        canBeDeleted = false;
                }
                if (canBeDeleted)
                    RelatedTypes.Remove(t);
            }
            foreach (var t in processor.RelatedTypes)
            {
                bool canBeDeleted = true;
                foreach (var p in Processors)
                {
                    if (p.RelatedEvents.Contains(t))
                        canBeDeleted = false;
                }
                if (canBeDeleted)
                    RelatedEvents.Remove(t);
            }
            return true;
        }
    }

    public void Update()
    {
        if (stateEvent is not null)
        {
            if (States.IsValid(stateEvent.Value))
            {
                foreach (var p in Processors)
                    p.Update();
                foreach (var p in SingleShots)
                    p.Update();
                SingleShots.Clear();
            }
        }
        else
        {
            foreach (var p in Processors)
                p.Update();
            foreach (var p in SingleShots)
                p.Update();
            SingleShots.Clear();
        }
    }

    public Enumerator GetEnumerator() => new(this);


    public ref struct Enumerator(Group group)
    {
        Group group = group;
        List<Processor>.Enumerator currentEnumerator = group.Processors.GetEnumerator();
        bool processedFirstOnes = false;

        public Processor Current => currentEnumerator.Current;

        public bool MoveNext()
        {
            if (!processedFirstOnes && currentEnumerator.MoveNext())
                return true;
            else if (!processedFirstOnes && !currentEnumerator.MoveNext())
            {
                processedFirstOnes = true;
                currentEnumerator = group.SingleShots.GetEnumerator();
                return currentEnumerator.MoveNext();
            }
            else if (processedFirstOnes && currentEnumerator.MoveNext())
                return true;
            else
                return false;
        }
    }

    public override string ToString()
    {
        var b = new StringBuilder().Append('[');
        if (Processors.Count + SingleShots.Count == 0)
            return b.Append(']').ToString();
        bool start = true;

        foreach (var e in this)
            if (start)
            {
                start = false;
                b.Append(e);
            }
            else
                b.Append(", ").Append(e);
        return b.Append(']').ToString();
    }

}