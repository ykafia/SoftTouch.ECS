using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS.Scheduling;

public class ProcessorGroup
{
    public HashSet<Type> RelatedTypes { get; } = [];
    public HashSet<Type> RelatedEventWriterTypes { get; } = [];
    readonly List<Processor> processors = [];
    public int Count => processors.Count;
    public void Update()
    {
        foreach (var p in processors)
            if (p.CanRun)
                p.Update();
    }


    public bool TryAdd(Processor processor)
    {
        // check if the processor has any related EventWriter
        // writing cannot be done in parallel so the same event writers should be in the same group.
        bool related = false;
        foreach (var ert in processor.RelatedEventWriterTypes)
            if (RelatedEventWriterTypes.Contains(ert))
            {
                related = true;
                break;
            }
        // check if the processor has any related types that are already in the group
        // If the there are types processed that already are present in the group, they should happen together
        if (!related)
            foreach (var rt in processor.RelatedTypes)
                if (RelatedTypes.Contains(rt))
                {
                    related = true;
                    break;
                }
        // If there are related types they should be in the same group.
        if (!related && processors.Count > 0)
            return false;
        else
        {
            foreach (var rt in processor.RelatedTypes)
                RelatedTypes.Add(rt);
            foreach (var ert in processor.RelatedEventWriterTypes)
                RelatedEventWriterTypes.Add(ert);
            processors.Add(processor);
            return true;
        }
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", processors.Select(x => x.GetType().Name))}]";
    }
}