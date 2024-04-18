
using System.Runtime.CompilerServices;

namespace SoftTouch.ECS.States;



public class WorldStates()
{
    // TODO: 
    // readonly HashSet<Type> Changed = [];
    Dictionary<Type, uint> nextStates = [];
    Dictionary<Type, uint> states = [];

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

    public void Update()
    {
        (nextStates, states) = (states, nextStates);
        nextStates = nextStates.ToDictionary(p => p.Key, p => 0U);
    }

    internal bool IsValid(StateEvent stateEvent)
    {
        
        return false;
    }
}