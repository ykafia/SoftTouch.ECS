using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling2;

public class ProcessorGroup
{
    public HashSet<Type> RelatedTypes { get; } = [];
    public HashSet<Type> RelatedEventWriterTypes { get; } = [];
    readonly List<Processor> processors = [];
    public void Update()
    {
        foreach (var p in processors)
            p.Update();
    }


    public bool TryAdd(Processor processor)
    {
        // check if the processor has any related EventWriter, writing cannot be done in parallel so the same event writers should be in the same group.
        bool related = false;
        foreach(var ert in processor.RelatedEventWriterTypes)
            if(RelatedEventWriterTypes.Contains(ert))
            {
                related = true;
                break;   
            }
        // check if the processor has any related types that are already in the group
        if(!related)
            foreach(var rt in processor.RelatedTypes)
                if(RelatedTypes.Contains(rt))
                    return false;
        foreach(var rt in processor.RelatedTypes)
            RelatedTypes.Add(rt);
        foreach(var ert in processor.RelatedEventWriterTypes)
            RelatedEventWriterTypes.Add(ert);
        processors.Add(processor);
        return true;
    }
}