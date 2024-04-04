using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public struct ProcessorGroup
{
    HashSet<Type> RelatedTypes;
    List<Processor> Processors;

    public readonly int Count => Processors.Count;

    public ProcessorGroup()
    {
        RelatedTypes = [];
        Processors = [];
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
    public bool TryRemove<TProcessor>(TProcessor processor)
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

    public void Update()
    {
        foreach (var p in Processors)
        {
            if (p.Enabled)
            {
                p.Update();
                if (p.RunAndDisable)
                    p.Enabled = false;
            }
        }
    }

    public List<Processor>.Enumerator GetEnumerator() => Processors.GetEnumerator();
}