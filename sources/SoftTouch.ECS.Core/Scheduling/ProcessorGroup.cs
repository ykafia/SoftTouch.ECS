using System.Text;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;
using SoftTouch.ECS.States;

namespace SoftTouch.ECS.Scheduling;


public class Group(StateTransition? transition = null)
{
    HashSet<Type> relatedEventWriterTypes = [];
    HashSet<Type> relatedTypes = [];
    List<Processor> processors = [];
    List<Processor> singleShots = [];
    public StateTransition? StateEvent { get; } = transition;

    public int Count => processors.Count;

    internal Group With<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach (var t in p.RelatedTypes)
            relatedTypes.Add(t);
        foreach (var t in p.RelatedEventWriterTypes)
            relatedEventWriterTypes.Add(t);
        processors.Add(p);
        return this;
    }

    /// <summary>
    /// Tries to add a processor in the group, if the processor has types related to the group then it can be added else it goes in another group.
    /// Additionally, it checks if the queries are Event readers and matches specific rules
    /// </summary>
    /// <typeparam name="TProcessor"></typeparam>
    /// <param name="p">Processor to add to the group</param>
    /// <returns>True if the processor has been added to the group, false if not.</returns>
    public bool TryAdd<TProcessor>(TProcessor p, StateTransition? st = null)
        where TProcessor : Processor
    {
        var related = false;
        // Checks if p write events of specific type, 
        // The rule is that two event writers cannot happen in parallel, so they have to happen sequentially.
        // They have to happen in the same group.
        foreach (var t in p.RelatedEventWriterTypes)
            if (relatedEventWriterTypes.Contains(t))
                return false;


        // TODO: check if this approach is better
        // using ReusableList<Type> processorRelatedTypes = [
        //     ..p.RelatedTypes, 
        //     ..p.RelatedEventWriterTypes
        // ];
        // using ReusableList<Type> groupRelatedTypes = [
        //     ..this.relatedTypes,
        //     ..this.relatedEventWriterTypes
        // ];
        if (p.RelatedTypes.Count == 0 && relatedTypes.Count == 0)
        {
            processors.Add(p);
            return true;
        }
        else if (p.RelatedTypes.Count > 0 && relatedTypes.Count > 0)
        {
            foreach (var t in p.RelatedTypes)
            {
                if (relatedTypes.Contains(t))
                {
                    related = true;
                    processors.Add(p);
                    foreach (var a in p.RelatedTypes)
                        relatedTypes.Add(a);
                    foreach (var a in p.RelatedEventWriterTypes)
                        relatedEventWriterTypes.Add(a);
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
        if (!processors.Contains(processor))
            return false;
        else
        {
            processors.Remove(processor);
            foreach (var t in processor.RelatedTypes)
            {
                bool canBeDeleted = true;
                foreach (var p in processors)
                {
                    if (p.RelatedTypes.Contains(t))
                        canBeDeleted = false;
                }
                if (canBeDeleted)
                    relatedTypes.Remove(t);
            }
            foreach (var t in processor.RelatedTypes)
            {
                bool canBeDeleted = true;
                foreach (var p in processors)
                {
                    if (p.RelatedEventWriterTypes.Contains(t))
                        canBeDeleted = false;
                }
                if (canBeDeleted)
                    relatedEventWriterTypes.Remove(t);
            }
            return true;
        }
    }

    public void Update()
    {
        foreach (var p in processors)
            p.Update();
        foreach (var p in singleShots)
            p.Update();
        singleShots.Clear();
    }

    public Enumerator GetEnumerator() => new(this);


    public ref struct Enumerator(Group group)
    {
        Group group = group;
        List<Processor>.Enumerator currentEnumerator = group.processors.GetEnumerator();
        bool processedFirstOnes = false;

        public Processor Current => currentEnumerator.Current;

        public bool MoveNext()
        {
            if (!processedFirstOnes && currentEnumerator.MoveNext())
                return true;
            else if (!processedFirstOnes && !currentEnumerator.MoveNext())
            {
                processedFirstOnes = true;
                currentEnumerator = group.singleShots.GetEnumerator();
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
        if (processors.Count + singleShots.Count == 0)
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