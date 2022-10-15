namespace ECSharp;

public abstract class ComponentUpdate
{
    public ArchetypeRecord EntityRecord {get;set;}
    public abstract void UpdateRecord();
}

public class ComponentAdd<T> : ComponentUpdate where T : struct
{
    public T Value {get;set;}

    public ComponentAdd(in T val, ArchetypeRecord e)
    {
        Value = val;
        EntityRecord = e;
    }

    public override void UpdateRecord()
    {
        EntityRecord.AddComponent(Value);
    }
}
public class ComponentRemove<T> : ComponentUpdate where T : struct
{
    public Type ToRemove => typeof(T);
    public ComponentRemove(ArchetypeRecord e)
    {
        EntityRecord = e;
    }

    public override void UpdateRecord()
    {
        EntityRecord.RemoveComponent<T>();
    }
}


public class UpdateQueue : Queue<ComponentUpdate>
{
    public void ExecuteUpdates()
    {
        while(TryDequeue(out var e))
        {
            e.UpdateRecord();
        }
    }
}