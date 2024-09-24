using Microsoft.Extensions.ObjectPool;

namespace SoftTouch.ECS;

public enum ComponentOperation
{
    Add,
    Remove
}

public record struct ComponentUpdate(ComponentBox Component, ComponentOperation Operation);