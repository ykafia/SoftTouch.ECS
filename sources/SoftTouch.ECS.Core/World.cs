using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS
{
    public sealed partial class World
    {

        public WorldResources Resources = new();
        public List<Entity> Entities = new();

        public ArchetypeList Archetypes = new();

        public WorldCommands Commands => Resources.Get<WorldCommands>();

        public Entity this[int id]
        {
            get => Entities[new EntityId(id)];
        }

        public World()
        {
            Archetypes.Add(new(), Archetype.CreateEmpty(this));
            Resources.Set(new WorldTimer());
            Resources.Set(new WorldCommands(this));
        }

        

        internal Archetype GenerateArchetype(ArchetypeID types, IEnumerable<ComponentArray> components)
        {
            if (!Archetypes.ContainsKey(types))
            {
                Archetypes.Add(types, new Archetype(components, this));
                return Archetypes[types];
            }
            else
                return Archetypes[types];

        }
        internal Archetype GenerateArchetype(ArchetypeID types, List<ComponentBase> components)
        {
            if (!Archetypes.ContainsKey(types))
            {
                Archetypes.Add(types, new Archetype(components, this));
                return Archetypes[types];
            }
            else
                return Archetypes[types];
        }

        public void BuildGraph()
        {
            var stor = Archetypes.Values;
            foreach (var arch in Archetypes.Values)
            {
                foreach (var x in stor
                    .Where(x => x.ID.IsAddedType(arch.ID))
                    .Select(other => { arch.TypeExcept(other, out var types); return (types[0], other);}))
                {
                    arch.Edges.Add[x.Item1] = x.other;
                }
                foreach (var x in stor
                    .Where(x => x.ID.IsRemovedType(arch.ID))
                    .Select(other => { arch.TypeExcept(other, out var types); return (types[0], other);})
                )
                {
                    arch.Edges.Remove[x.Item1] = x.other;
                }
            }
        }

        public IEnumerable<Archetype> QueryArchetypes(ArchetypeID types)
        {
            for (int i = 0; i< Archetypes.Count; i++)
            {
                if (Archetypes.Values[i].ID.IsSupersetOf(types.Span))
                    yield return Archetypes.Values[i];
            }
        }
    }
}