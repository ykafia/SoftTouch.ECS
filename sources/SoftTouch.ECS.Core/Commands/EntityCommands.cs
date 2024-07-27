using System.Runtime.CompilerServices;
using SoftTouch.ECS.Components;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS;


public record struct EntityCommands(EntityUpdate Entity, WorldCommands Commands)
{
    public readonly Entity Id => Entity.Entity;
}

public static class EntityCommandsExtensions
{
    public static EntityCommands Insert<T>(this EntityCommands commands)
        where T : struct
    {
        commands.Entity.Add<T>(default);
        return commands;
    }
    public static EntityCommands Insert<T>(this EntityCommands commands, in T component)
        where T : struct
    {
        commands.Entity.Add(component);
        return commands;
    }
    public static EntityCommands SetParent(this EntityCommands commands, in Entity entity)
    {
        commands.Entity.Add<Parent>(entity);
        return commands;
    }
    public static EntityCommands PushChildren(this EntityCommands commands, List<Entity> entities)
    {
        commands.Entity.Add<Children>(entities);
        return commands;
    }
}