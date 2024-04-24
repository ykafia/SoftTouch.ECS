
using System.Runtime.CompilerServices;
using Microsoft.Extensions.ObjectPool;

namespace SoftTouch.ECS.States;


public record struct StateChange(uint Exits, uint Enters, Type StateType);

public class WorldStates()
{
    // TODO: 
    // readonly HashSet<Type> Changed = [];
    Dictionary<Type, uint> nextStates = [];
    Dictionary<Type, uint> states = [];

    List<StateChange> stateChanges = [];

    public T Get<T>() where T : Enum
    {
        var state = states[typeof(T)];
        return Unsafe.As<uint, T>(ref state);
    }
    public T GetNew<T>() where T : Enum
    {
        var state = nextStates[typeof(T)];
        return Unsafe.As<uint, T>(ref state);
    }

    public void Set<T>(T value)
        where T : Enum
    {
        if(!states.TryGetValue(typeof(T), out _))
            states[typeof(T)] = 0;
        nextStates[typeof(T)] = Convert.ToUInt32(value);
    }

    public bool Remove<T>()
    {
        return states.Remove(typeof(T)) && nextStates.Remove(typeof(T));
    }

    internal void Update()
    {
        stateChanges.Clear();
        foreach(var t in states.Keys)
        {
            
        }
        (nextStates, states) = (states, nextStates);
        nextStates = nextStates.ToDictionary(p => p.Key, p => 0U);
    }

    internal bool IsValid(StateEvent stateEvent)
    {
        
        return false;
    }

}