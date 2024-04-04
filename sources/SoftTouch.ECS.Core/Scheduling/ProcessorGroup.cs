using System.Text;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public struct ProcessorGroup
{
    HashSet<Type> RelatedTypes;
    List<Processor> Processors;
    List<Processor> SingleShots;

    public readonly int Count => Processors.Count;

    public ProcessorGroup()
    {
        RelatedTypes = [];
        Processors = [];
        SingleShots = [];
    }

    internal readonly ProcessorGroup With<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        foreach(var t in p.RelatedTypes)
            RelatedTypes.Add(t);
        Processors.Add(p);
        return this;
    }

    public readonly bool TryAdd<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        var related = false;
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
                    break;
                }
            }
            return related;
        }
        else
        {
            return false;
        }
    }
    public readonly bool TryRemove<TProcessor>(TProcessor processor)
        where TProcessor : Processor
    {
        if(!Processors.Contains(processor))
            return false;
        else{
            Processors.Remove(processor);
            foreach(var t in processor.RelatedTypes)
            {
                bool canBeDeleted = true;
                foreach(var p in Processors)
                {
                    if(p.RelatedTypes.Contains(t))
                        canBeDeleted = false;
                }
                if(canBeDeleted)
                    RelatedTypes.Remove(t);
            }
            return true;
        }
    }

    public readonly void Update()
    {
        foreach (var p in Processors)
            p.Update();
        foreach (var p in SingleShots)
            p.Update();
        SingleShots.Clear();
    }

    public readonly Enumerator GetEnumerator() => new(this);


    public ref struct Enumerator(ProcessorGroup group)
    {
        ProcessorGroup group = group;
        List<Processor>.Enumerator currentEnumerator = group.Processors.GetEnumerator();
        bool processedFirstOnes = false;

        public Processor Current => currentEnumerator.Current;

        public bool MoveNext()
        {
            if(!processedFirstOnes && currentEnumerator.MoveNext())
                return true;
            else if(!processedFirstOnes && !currentEnumerator.MoveNext())
            {
                processedFirstOnes = true;
                currentEnumerator = group.SingleShots.GetEnumerator();
                return currentEnumerator.MoveNext();
            }
            else if(processedFirstOnes && currentEnumerator.MoveNext())
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