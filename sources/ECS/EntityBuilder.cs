using System;
using System.Linq;
using System.Collections.Generic;
using ECSharp.ComponentData;
using ECSharp.Arrays;

namespace ECSharp
{
    public struct EntityBuilder : IEntity
    {
        public Entity Entity;
        public World World => Entity.World;
        public EntityBuilder(Entity e)
        {
            Entity = e;
        }
        public EntityBuilder With<T>(in T component) where T : struct
        {
            if(World[Entity].Has<T>())
                World[Entity].Set(component);
            else
                World[Entity.Index].Add(component);
            World.BuildGraph();
            return this;
        }

        public Entity GetEntity()
        {
            return Entity;
        }
    }
}