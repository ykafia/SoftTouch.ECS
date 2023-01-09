using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;

namespace SoftTouch.ECS
{
    public class Archetype
    {
        public static Archetype CreateEmpty(World w) => new(new List<ComponentBase>(),w);

        readonly World world;
        public Dictionary<Type, ComponentList> Storage = new();
        public Dictionary<long,int> EntityID = new();
        public bool HasEntities => EntityID.Count > 0;

        public ArchetypeID ID = new();

        public ArchetypeEdges Edges = new();

        public int Length => EntityID.Count;

        public Archetype(IEnumerable<ComponentBase> components, World w)
        {
            foreach(var c in components)
            {
                Storage[c.GetComponentType()] = c.EmptyArray();
            }
            ID = new ArchetypeID(components.Select(x => x.GetComponentType()).ToHashSet());
            world = w;
        }

        public Archetype(IEnumerable<ComponentList> componentArrays, World w)
        {
            foreach(var ca in componentArrays)
            {
                Storage[ca.ComponentType] = ca.New();
            };
            ID = new ArchetypeID(componentArrays.Select(x => x.ComponentType).ToHashSet());
            world = w;
            
        }

        public ArchetypeRecord this[int i]
        {
            get => world[EntityID[i]];
        }

        public bool IsStrictSupersetOf(Archetype t) => ID.IsStrictSupersetOf(t.ID);
        public bool IsStrictSupersetOf(params Type[] types) => ID.IsStrictSupersetOf(new(types));
        public bool IsStrictSubsetOf(Archetype t) => ID.IsStrictSubsetOf(t.ID);
        public bool IsStrictSubsetOf(params Type[] types) => ID.IsStrictSubsetOf(new(types));

        public IEnumerable<Type> TypeIntersect(Archetype t) => this.ID.Intersect(t.ID);
        public IEnumerable<Type> TypeExcept(Archetype t) => this.ID.Except(t.ID);

        public Span<T> GetComponentSpan<T>() where T : struct
        {
            return ((ComponentList<T>)Storage[typeof(T)]).AsSpan();
        }
        internal ComponentList<T> GetComponentArray<T>() where T : struct
        {
            return (ComponentList<T>)Storage[typeof(T)];
        }
        public void GetEntityComponent<T>(int i, out T c) where T : struct
        {
            c = ((ComponentList<T>)Storage[typeof(T)])[i];
        }

        public void AddComponent<T>(in T component, long entity) where T : struct
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                var array = GetComponentArray<T>();
                array.Add(component);
                EntityID.Add(entity,array.Count);
            }
        }

        public void RemoveEntity(Entity e)
        {
            if(EntityID.Count > 0) 
                EntityID.Remove(e.Index);
        }
        public ComponentList<T> GetComponentList<T>() where T : struct
        {
            return (ComponentList<T>)Storage[typeof(T)];
        }
        public void SetComponent<T>(int index, in T component) where T : struct
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                GetComponentSpan<T>()[index] = component;
            }
        }

        internal void AddEntity(Entity entity) => EntityID.Add(entity.Index, Length);

        public override string ToString()
        {
            var result = 
            new StringBuilder()
                .Append("Type : [")
                .Append(string.Join(";", Storage.Keys.Select(x => x.Name)??new List<string>()))
                .Append(']')
                .AppendLine()
                .Append("Storages : [")
                .Append(string.Join(";",Storage.Values.Select(x => x.ToString())))
                .Append(']');
            return result.ToString();
        }

        

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}