using System.Collections;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS;


public enum StateAction
{
    OnEnter,
    OnExit
}

public record struct StateEvent(StateAction Action, Type StateType, uint State);

public struct ProcessorsInfo : ICollection<Processor>, IDisposable
{
    public ReusableList<Processor> Processors { get; set; }
    StateEvent? stateEvent;

    public ProcessorsInfo()
    {
        Processors = [];
    }

    public ProcessorsInfo WhenEnters<TState>(TState state) 
        where TState : Enum
    {
        stateEvent = new StateEvent(StateAction.OnEnter, typeof(TState), Convert.ToUInt32(state));
        return this;
    }
    public ProcessorsInfo WhenExits<TState>(TState state)
        where TState : Enum
    {
        stateEvent = new StateEvent(StateAction.OnExit, typeof(TState), Convert.ToUInt32(state));
        return this;
    }

    public readonly int Count => Processors.Count;

    public readonly bool IsReadOnly => false;
    public readonly void Add(Processor item)
        => Processors.Add(item);
    public readonly bool Remove(Processor item)
        => Processors.Remove(item);
    public readonly void Clear()
        => Processors.Clear();
    public readonly bool Contains(Processor item)
        => Processors.Contains(item);
    public readonly void CopyTo(Processor[] array, int arrayIndex)
        => Processors.CopyTo(array, arrayIndex);
    public readonly IEnumerator<Processor> GetEnumerator()
        => Processors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public readonly void Dispose()
        => Processors.Dispose();
}