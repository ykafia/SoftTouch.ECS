namespace SoftTouch.ECS;

public class ProcessorPool
{
    List<List<IProcessor>> processorLists = new();

    Dictionary<IProcessor, List<IProcessor>> Lookup = new();

    public ProcessorPool()
    {
        processorLists.Add(new());
    }
    public void Add(IProcessor p)
    {
        processorLists[0].Add(p);
        Lookup[p] = processorLists[0];
    }
    public void Remove(IProcessor p)
    {
        Lookup[p].Remove(p);
        Lookup.Remove(p);
    }

    public void MoveAfter(string label, IProcessor after)
    {
        // var afterIndex = processorLists.IndexOf(Lookup[after]);
        // foreach(var pl in processorLists)
        // foreach(var p in pl)
        //     if(processorLists.IndexOf(p))
        throw new NotImplementedException();
    }

    public void MoveAfter(IProcessor before, IProcessor after)
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

    internal void Execute(bool parallel = true)
    {
        foreach(var pl in processorLists)
        {
            if (parallel)
                pl.AsParallel().ForAll(x => x.Update());
            else
                foreach (var p in pl) p.Update();
        }
    }
}