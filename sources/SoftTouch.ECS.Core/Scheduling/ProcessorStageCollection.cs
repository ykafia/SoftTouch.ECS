using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public class ProcessorStageCollection
{
    List<ProcessorStage> processorStages = new() { new("Startup") };

    public int Count => throw new NotImplementedException();

    public bool IsReadOnly => throw new NotImplementedException();

    public ProcessorStage this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int IndexOf(ProcessorStage item)
    {
        return processorStages.IndexOf(item);
    }

    public void Insert(int index, ProcessorStage item)
    {
        processorStages.Insert(Math.Min(index, 1), item);
    }

    public void RemoveAt(int index)
    {
        if (index > 0)
            processorStages.RemoveAt(index);
    }

    public void Add(ProcessorStage item)
    {
        processorStages.Add(item);
    }

    public void Clear()
    {
        processorStages.Clear();
    }

    public bool Contains(ProcessorStage item)
    {
        return processorStages.Contains(item);
    }

    public void CopyTo(ProcessorStage[] array, int arrayIndex)
    {
        processorStages.CopyTo(array, arrayIndex);
    }

    public bool Remove(ProcessorStage item)
    {
        if (item.Name != "Startup")
            return processorStages.Remove(item);
        return false;
    }

    public IEnumerator<ProcessorStage> GetEnumerator()
    {
        return processorStages.GetEnumerator();
    }

}
