namespace SoftTouch.ECS;

public class ProcessorPool
{
    List<List<Processor>> processorLists = new();

    Dictionary<Processor, List<Processor>> Lookup = new();

    public ProcessorPool()
    {
        processorLists.Add(new());
    }
    public void Add(Processor p)
    {
        processorLists[0].Add(p);
        Lookup[p] = processorLists[0];
    }
    public void Remove(Processor p)
    {
        Lookup[p].Remove(p);
        Lookup.Remove(p);
    }

    public void MoveAfter(string label, Processor after)
    {
        // var afterIndex = processorLists.IndexOf(Lookup[after]);
        // foreach(var pl in processorLists)
        // foreach(var p in pl)
        //     if(processorLists.IndexOf(p))
        throw new NotImplementedException();
    }

    public void MoveAfter(Processor before, Processor after)
    {
        if (processorLists.IndexOf(Lookup[after]) <= processorLists.IndexOf(Lookup[before]))
        {
            Lookup[after].Remove(after);
            if (processorLists.Count < processorLists.IndexOf(Lookup[before]) + 1)
                processorLists.Add(new());
            var newList = processorLists[processorLists.IndexOf(Lookup[before]) + 1];
            newList.Add(after);
            Lookup[after] = newList;
        }
    }

    internal void Execute()
    {
        foreach(var pl in processorLists)
        {
            pl.AsParallel().ForAll(x => x.Update());
        }
    }
}