namespace SoftTouch.ECS;


/// <summary>
/// Describes a state by its type, its value and wether it's on entry/on active or on exit.
/// </summary>
public record struct StateTransition(StateStatus Status, uint State, Type StateType);
