using System;
using System.Collections.Generic;
using System.Linq;

namespace WonkECS
{
    public class World
    {
        public Dictionary<long, ArchetypeRecord> Entities = new();

        public Dictionary<ArchetypeID, Archetype> Archetypes = new(new ArchetypeIDComparer());

        public List<Processor> Processors { get; set; } = new();

        public ArchetypeRecord this[long id]
        {
            get {return Entities[id];}
            set { Entities[id] = value; }
        }

        public EntityBuilder CreateEntity()
        {
            var e = new EntityBuilder { Entity = new Entity { Index = Entities.Count }, World = this };
            Entities[e.Entity.Index] = new();
            return e;
        }

        public ArchetypeRecord GetOrCreateRecord(ArchetypeID types, EntityBuilder e)
        {
            if (Archetypes.TryGetValue(types, out Archetype? a) && e.Entity != null)
            {
                return new ArchetypeRecord { Entity = e.Entity, ArchetypeIndex = a.Length, Archetype = a };
            }
            else
            {
                throw new NotImplementedException("Cannot generate record");
            }
        }

        internal Archetype GenerateArchetype(ArchetypeID types, List<ComponentBox> components)
        {
            if (!Archetypes.ContainsKey(types))
            {
                Archetypes.Add(types, new Archetype(components));
                return Archetypes[types];
            }
            else
                return Archetypes[types];

        }

        public void BuildGraph()
        {
            var stor = Archetypes.Values.ToList();
            foreach (var arch in Archetypes.Values)
            {
                stor
                    .Where(x => x.ID.IsAddedType(arch.ID))
                    .Select(other => (arch.TypeExcept(other).First(), other))
                    .ToList()
                    .ForEach(x => arch.Edges.Add[x.Item1] = x.other);
                stor
                    .Where(x => x.ID.IsRemovedType(arch.ID))
                    .Select(other => (other.TypeExcept(arch).First(), other))
                    .ToList()
                    .ForEach(x => arch.Edges.Remove[x.Item1]= x.other);
            }
        }

        public List<Archetype> QueryArchetypes(ArchetypeID types)
        {
            return Archetypes
                .Where(arch => arch.Value.ID.IsSupersetOf(types))
                .Select(arch => arch.Value)
                .ToList();
        }


        public void Update()
        {
            Processors
                .ForEach(
                    x => x.Update(this)
                );
        }


        internal class ArchetypeIDComparer : IEqualityComparer<ArchetypeID>
        {
            public bool Equals(ArchetypeID x, ArchetypeID y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(ArchetypeID x)
            {
                return x.GetHashCode();
            }
        }
        // public void Add(Entity entity) => Entities.Add(entity,new ArchetypeRecord{Archetype = new Archetype(entity.Archetype)});
        // public void Remove(Entity entity) => Entities.Remove(entity);

    }
}