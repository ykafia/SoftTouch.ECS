using SoftTouch.ECS.Storage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;


public record EntityCommands(EntityUpdate Entity, WorldCommands Commands)
{
    public EntityCommands With<T1>(in T1 component1)
        where T1 : struct
    {
        Entity.Add(component1);
        return this;
    }
    public EntityCommands With<T1>()
        where T1 : struct
    {
        Entity.Add(default(T1));
        return this;
    }
}
