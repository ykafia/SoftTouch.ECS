using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;


public struct ProcessorGroup
{
    HashSet<Type> RelatedTypes;
    List<Processor> Processors;

    public ProcessorGroup()
    {
        RelatedTypes = new();
        Processors = new();
    }

    public bool TryAdd<TProcessor>(TProcessor p)
        where TProcessor : Processor
    {
        var related = false;
        foreach(var t in p.RelatedTypes)
        {
            if(RelatedTypes.Contains(t))
            {
                related = true;
                foreach(var a in p.RelatedTypes)
                    RelatedTypes.Add(a);
                break;
            }
        }
        return related;
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
        foreach(var p in Processors)
            p.Update();
    }
    
}