using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;
using SoftTouch.ECS.Storage;

namespace SoftTouch.ECS
{
    public partial class World
    {
        public Dictionary<Type, object> Resources = new();
        public List<Entity> Entities = new();

        public ArchetypeList Archetypes = new();
        public List<Processor> StartupProcessors { get; set; } = new();
        public ProcessorPool Processors { get; set; } = new();

        public bool IsRunning { get; private set; }

        public WorldCommands Commands { get; }

        public Entity this[int id]
        {
            get => Entities[new EntityId(id)];
        }

        public World()
        {
            Archetypes.Add(new(), Archetype.CreateEmpty(this));
            Resources.Add(typeof(WorldTimer), new WorldTimer());
            Commands = new(this);
        }

        public T GetResource<T>() where T : class
        {
            return (T)Resources[typeof(T)];
        }
        public void SetResource<T>(T res) where T : class
        {
            Resources[typeof(T)] = res;
        }

        //public EntityBuilder Spawn(
        //{
        //    var e = new EntityBuilder(new Entity(Entities.Count, this));
        //    Archetype.CreateEmpty(this).AddEntity(e.Entity);
        //    Entities[e.Entity.Index] = new(e.Entity,Archetype.CreateEmpty(this));
        //    return e;
        //}

        //public Entity GetOrCreateRecord(ArchetypeID types, EntityBuilder e)
        //{
        //    if (Archetypes.TryGetValue(types, out Archetype? a))
        //    {
        //        return new(e.Entity, a);
        //    }
        //    else
        //    {
        //        throw new NotImplementedException("Cannot generate record");
        //    }
        //}

        internal Archetype GenerateArchetype(ArchetypeID types, IEnumerable<ComponentList> components)
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


        public void AddArchetypeUpdate(ComponentUpdate update)
        {
            Commands.Enqueue(update);
        }

        public void Start()
        {
            foreach (Processor pa in StartupProcessors)
                pa.Update();
            Commands.ExecuteUpdates();
        }
        public virtual void Update(bool parallel = true)
        {
            Processors.Execute(parallel);
            Commands.ExecuteUpdates();
        }
    }
}