using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public class StageCollection : ICollection<Stage>
{
    readonly Startup startup = new();
    readonly List<Stage> Stages = [new Main()];
    readonly Extract extract = new();

    public Stage this[int i] => Stages[i];


    public int Count => Stages.Count;

    public bool IsReadOnly => false;

    public Stage Get<TStage>() where TStage : Stage
    {
        if(startup is TStage)
            return startup;
        else if(extract is TStage)
            return extract;
        foreach(var stage in Stages)
            if(stage is TStage)
                return stage;
        throw new ArgumentException($"Stage {typeof(TStage)} not present in the list");
    }

    public int IndexOf(Stage item)
    {
        return Stages.IndexOf(item);
    }

    public void Insert(int index, Stage item)
    {
        Stages.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
        Stages.RemoveAt(index);
    }

    public void Add(Stage item)
    {
        Stages.Add(item);
    }

    public void Clear()
    {
        foreach(var s in Stages)
            s.Dispose();
        Stages.Clear();
    }

    public bool Contains(Stage item)
    {
        if(item is Startup || item is Extract)
            return true;
        else 
            return Stages.Contains(item);
    }

    public void CopyTo(Stage[] array, int arrayIndex)
    {
        Stages.CopyTo(array, arrayIndex);
    }

    public bool Remove(Stage item)
    {
        return item switch 
        {
            Startup or Extract or Main => false,
            _ => Stages.Remove(item)
        };
    }

    public List<Stage>.Enumerator GetEnumerator()
    {
        return Stages.GetEnumerator();
    }

    IEnumerator<Stage> IEnumerable<Stage>.GetEnumerator()
    {
        return Stages.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Stages.GetEnumerator();
    }
}
