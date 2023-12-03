﻿using SoftTouch.ECS.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS;

public readonly struct EntityCommands(GenerationalEntity e, WorldCommands commands)
{
    readonly GenerationalEntity entity = e;
    readonly WorldCommands commands = commands;

    public EntityCommands With<T1>(in T1 component1)
        where T1 : struct
    {
        //entity.Add(component1);
        throw new NotImplementedException();
        return this;
    }
    public EntityCommands With<T1>()
        where T1 : struct
    {
        //entity.Add(default(T1));
        throw new NotImplementedException();
        return this;
    }

}
