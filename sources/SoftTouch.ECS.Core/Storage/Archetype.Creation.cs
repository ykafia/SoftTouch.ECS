using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{
    public Archetype()
    {
        World = null!;
        EntityLookup = [];
    }

    public Archetype(World world, Entity entity, ReadOnlySpan<ComponentBox> components)
    {
        World = world;
        EntityLookup = [entity.Index];
        foreach (var component in components)
            Storage[component.ComponentType] = component.ToArray();
        ID = new(components);
    }
}