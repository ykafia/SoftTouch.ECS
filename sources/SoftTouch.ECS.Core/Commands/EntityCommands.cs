using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public struct EntityCommands
{
    Entity entity;

    public EntityCommands(in Entity e)
    {
        entity = e;
    }

    public static implicit operator EntityCommands(Entity e) => new(e);
    public static implicit operator Entity(EntityCommands commands) => commands.entity;
    
    public EntityCommands With<T1>(in T1 component1)
        where T1 : struct
    {
        entity.Add(component1);
        return this;
    }
    public EntityCommands With<T1>()
        where T1 : struct
    {
        entity.Add(default(T1));
        return this;
    }
}
