
using System.Runtime.CompilerServices;
using Microsoft.Extensions.ObjectPool;

namespace SoftTouch.ECS.States;

/// <summary>
/// Class describing states in the world
/// </summary>
public class WorldStates()
{
    readonly List<Type> types = [];
    Dictionary<Type, uint> nextStates = [];
    Dictionary<Type, uint> states = [];

    readonly List<StateTransition> stateChanges = [];

    public IReadOnlyList<StateTransition> StateChanges => stateChanges;

    /// <summary>
    /// Gets the value of the state based on the type
    /// </summary>
    /// <typeparam name="T">Type of the state</typeparam>
    /// <returns>Value of the state</returns>
    public T Get<T>() where T : Enum
    {
        var state = states[typeof(T)];
        return Unsafe.As<uint, T>(ref state);
    }
    /// <summary>
    /// Gets the value of the new state based on the type
    /// </summary>
    /// <typeparam name="T">Type of the state</typeparam>
    /// <returns>Value of the new state</returns>
    public T GetNew<T>() where T : Enum
    {
        var state = nextStates[typeof(T)];
        return Unsafe.As<uint, T>(ref state);
    }
    /// <summary>
    /// Sets the value of the state
    /// </summary>
    /// <param name="value">Type of the state to change</param>
    /// <typeparam name="T"></typeparam>
    public void Set<T>(T value)
        where T : Enum
    {
        if (!states.TryGetValue(typeof(T), out _))
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
        types.Clear();
        types.AddRange(states.Keys);
        foreach (var t in types)
            if (states[t] != nextStates[t])
            {
                stateChanges.Add(new(StateStatus.OnExit, states[t], t));
                stateChanges.Add(new(StateStatus.OnEnter, nextStates[t], t));
            }
            else
                stateChanges.Add(new(StateStatus.OnActive, states[t], t));
        (nextStates, states) = (states, nextStates);

        foreach (var (k, v) in states)
            nextStates[k] = v;
    }

    public bool IsValid(in StateTransition e)
        => e.Status switch
        {
            StateStatus.OnActive => stateChanges.Contains(e) || stateChanges.Contains(e with { Status = StateStatus.OnEnter }),
            _ => stateChanges.Contains(e),
        };
}