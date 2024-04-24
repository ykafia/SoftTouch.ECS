namespace SoftTouch.ECS;

public record struct StateTransition(StateStatus Status, uint State, Type StateType);
