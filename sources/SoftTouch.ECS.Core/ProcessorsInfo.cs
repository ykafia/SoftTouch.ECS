using System.Collections;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.Processors;

namespace SoftTouch.ECS;


public enum StateStatus
{
    OnActive,
    OnEnter,
    OnExit,
}

public struct ProcessorsInfo : IDisposable
{
    public ReusableList<Processor> Processors { get; set; }
    StateTransition? stateEvent;

    public ProcessorsInfo()
    {
        Processors = [];
    }

    public ProcessorsInfo WhenEnters<TState>(StateStatus action, TState state)
        where TState : Enum
    {
        stateEvent = new StateTransition(StateStatus.OnEnter, Convert.ToUInt32(state), typeof(TState));
        return this;
    }
    public ProcessorsInfo WhenEnters<TState>(TState state) 
        where TState : Enum
    {
        stateEvent = new StateTransition(StateStatus.OnEnter,Convert.ToUInt32(state), typeof(TState));
        return this;
    }
    public ProcessorsInfo WhenExits<TState>(TState state)
        where TState : Enum
    {
        stateEvent = new StateTransition(StateStatus.OnExit, Convert.ToUInt32(state), typeof(TState));
        return this;
    }
    public ProcessorsInfo WhenActive<TState>(TState state)
        where TState : Enum
    {
        stateEvent = new StateTransition(StateStatus.OnActive, Convert.ToUInt32(state), typeof(TState));
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
    public readonly Span<Processor>.Enumerator GetEnumerator()
        => Processors.GetEnumerator();

    public readonly void Dispose()
        => Processors.Dispose();
}