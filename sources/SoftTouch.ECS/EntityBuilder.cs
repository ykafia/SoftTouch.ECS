using System;
using System.Linq;
using System.Collections.Generic;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Arrays;

namespace SoftTouch.ECS
{
    public struct EntityBuilder : IEntity
    {
        public Entity Entity;
        public World World => Entity.World;
        public EntityBuilder(Entity e)
        {
            Entity = e;
        }
        public EntityBuilder WithBundle<T>(in T bundle) where T : struct, IBundle
        {
            bundle.AddBundle(this);
            return this;
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
        public EntityBuilder With<T>() where T : struct
        {
            if(World[Entity].Has<T>())
                World[Entity].Set(new T());
            else
                World[Entity.Index].Add(new T());
            World.BuildGraph();
            return this;
        }

        public Entity GetEntity()
        {
            return Entity;
        }
    }
}