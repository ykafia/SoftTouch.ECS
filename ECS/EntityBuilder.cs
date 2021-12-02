using System;
using System.Linq;
using System.Collections.Generic;

namespace WonkECS
{
    public class EntityBuilder : IEntity
    {
        public Entity? Entity;
        public World? World {get;set;}
        public HashSet<Type> ComponentTypes => Components.Keys.ToHashSet();

        public Dictionary<Type, ComponentBox> Components = new();

        public EntityBuilder With<T>(T component) where T : struct
        {
            if(!typeof(T).GetInterfaces().Contains(typeof(IEntity)))
            {
                Components[typeof(T)] = new ComponentBox<T>(component);
            }
            return this;
        }

        public void Build()
        {
            var types = new ArchetypeID(ComponentTypes);
            Archetype archetype = World.GenerateArchetype(types, Components.Values.ToList());
            foreach(var e in Components)
                archetype.Storage[e.Key].Add(e.Value);
            World[Entity.Index] = new ArchetypeRecord
            {
                Entity = Entity,
                ArchetypeIndex = archetype.Length,
                Archetype = archetype
            };
            archetype.EntityID.Add(Entity.Index);
            World.BuildGraph();
        }

        public override string ToString() => "[" + Entity.Index.ToString() + " : <" + string.Join(",",ComponentTypes.Select(x => x.Name)) +">]";

    }
}