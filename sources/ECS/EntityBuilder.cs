using System;
using System.Linq;
using System.Collections.Generic;
using ECSharp.ComponentData;
using ECSharp.Arrays;

namespace ECSharp
{
    public class EntityBuilder : IEntity
    {
        public Entity Entity;
        public World World => Entity.World;
        public EntityBuilder(Entity e)
        {
            Entity = e;
        }
        public EntityBuilder With<T>(in T component) where T : struct
        {
            World[Entity.Index].Add(component);
            World.BuildGraph();
            return this;
        }

        public Entity Build()
        {
            World.BuildGraph();
            return Entity;
        }

        public override string ToString() => "[" + Entity.Index.ToString() + " : <" + string.Join(",",ComponentTypes.Select(x => x.Name)) +">]";

    }
}