using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using ECSharp.Arrays;
using ECSharp.ComponentData;

namespace ECSharp
{
    public sealed class World
    {
        public Dictionary<Type, object> Resources = new();
        public SortedList<long, ArchetypeRecord> Entities = new();

        public SortedList<ArchetypeID, Archetype> Archetypes = new();
        public List<Processor> StartupProcessors { get; set; } = new();
        public List<Processor> Processors { get; set; } = new();
        public List<ProcessorAsync> AsyncProcessors { get; set; } = new();

        public bool IsRunning { get; private set; }

        public long FrameCount { get; private set; } = 0;

        UpdateQueue UpdateQueue = new();

        public ArchetypeRecord this[long id]
        {
            get { return Entities[id]; }
            set { Entities[id] = value; }
        }
        public ArchetypeRecord this[Entity e]
        {
            get { return Entities[e.Index]; }
            set { Entities[e.Index] = value; }
        }

        public World()
        {
            Archetypes.Add(new(), Archetype.CreateEmpty(this));
            Resources.Add(typeof(WorldTimer), new WorldTimer()); 
        }

        public T GetResource<T>() where T : class
        {
            return (T)Resources[typeof(T)];
        }
        public void SetResource<T>(T res) where T : class
        {
            Resources[typeof(T)] = res;
        }

        public EntityBuilder CreateEntity(string name = "")
        {
            var e = new EntityBuilder(new Entity(Entities.Count, this, name));
            Archetype.CreateEmpty(this).AddEntity(e.Entity);
            Entities[e.Entity.Index] = new ArchetypeRecord { Entity = e.Entity, Archetype = Archetype.CreateEmpty(this) };
            return e;
        }

        public ArchetypeRecord GetOrCreateRecord(ArchetypeID types, EntityBuilder e)
        {
            if (Archetypes.TryGetValue(types, out Archetype? a))
            {
                return new ArchetypeRecord { Entity = e.Entity, Archetype = a };
            }
            else
            {
                throw new NotImplementedException("Cannot generate record");
            }
        }

        internal Archetype GenerateArchetype(ArchetypeID types, IEnumerable<ComponentList> components)
        {
            if (!Archetypes.ContainsKey(types))
            {
                Archetypes.Add(types, new Archetype(components,this));
                return Archetypes[types];
            }
            else
                return Archetypes[types];
            
        }
        internal Archetype GenerateArchetype(ArchetypeID types, List<ComponentBase> components)
        {
            if (!Archetypes.ContainsKey(types))
            {
                Archetypes.Add(types, new Archetype(components,this));
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
                    .Select(other => (arch.TypeExcept(other).First(), other)))
                {
                    arch.Edges.Add[x.Item1] = x.other;
                }
                foreach (var x in stor
                    .Where(x => x.ID.IsRemovedType(arch.ID))
                    .Select(other => (other.TypeExcept(arch).First(), other))
                )
                {
                    arch.Edges.Remove[x.Item1] = x.other;
                }
            }
        }

        public IEnumerable<Archetype> QueryArchetypes(ArchetypeID types)
        {
            foreach(var arch in Archetypes.Values)
            {
                if(arch.ID.IsSupersetOf(types))
                    yield return arch;
            }
            // return Archetypes
            //     .Where(arch => arch.Value.ID.IsSupersetOf(types))
            //     .Select(arch => arch.Value);
        }
        public IEnumerable<Archetype> QueryArchetypes(params Type[] types)
        {
            var id = new ArchetypeID(types.ToHashSet());
            return Archetypes
                .Where(arch => arch.Value.ID.IsSupersetOf(id))
                .Select(arch => arch.Value);
        }

        public void Add(Processor p)
        {
            p.World = this;
            Processors.Add(p);
        }
        
        public void Add<T>() where T : Processor, new()
        {
            var p = new T
            {
                World = this
            };
            Processors.Add(p);
        }
        public void AddStartup<T>() where T : Processor, new()
        {
            var p = new T
            {
                World = this
            };
            StartupProcessors.Add(p);
        }
        public void Remove(Processor p) => Processors.Remove(p);


        public void AddArchetypeUpdate(ComponentUpdate update)
        {
            UpdateQueue.Enqueue(update);
        }


        public void StartAsync()
        {
            foreach (ProcessorAsync pa in AsyncProcessors)
                pa.Execute();
        }
        public void Start()
        {
            foreach(Processor pa in StartupProcessors)
                pa.Update();
            UpdateQueue.ExecuteUpdates();
        }
        public void Update()
        {
            foreach(var processor in Processors)
            {
                processor.Update();
            }
            UpdateQueue.ExecuteUpdates();
            FrameCount += 1;
        }

        public void Run(int framesToRun = 0)
        {
            IsRunning = true;
            // UpdateQueue.ExecuteUpdates();
            var watch = new Stopwatch();
            Start();
            while (IsRunning)
            {
                watch.Start();
                Update();
                if (framesToRun != 0 && FrameCount >= framesToRun)
                    IsRunning = false;
                watch.Stop();
                GetResource<WorldTimer>().Update(watch.Elapsed);
            }
            watch.Stop();
            IsRunning = false;
        }
    }
}