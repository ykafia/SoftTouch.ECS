using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ECSharp.Arrays;
using ECSharp.ComponentData;

namespace ECSharp
{
    public class Archetype
    {
        public Dictionary<Type, ComponentArrayBase> Storage = new();
        public List<long> EntityID = new();

        public ArchetypeID ID = new();

        public ArchetypeEdges Edges = new();

        public int Length => EntityID.Count;

        public Archetype(List<ComponentBase> components)
        {
            foreach(var c in components)
            {
                Storage[c.GetComponentType()] = c.EmptyArray();
            }
            ID = new ArchetypeID(components.Select(x => x.GetComponentType()));
        }


        public bool IsSupersetOf(Archetype t) => this.ID.IsSupersetOf(t.ID);
        public bool IsSubsetOf(Archetype t) => this.ID.IsSubsetOf(t.ID);
        public IEnumerable<Type> TypeIntersect(Archetype t) => this.ID.Intersect(t.ID);
        public IEnumerable<Type> TypeExcept(Archetype t) => this.ID.Except(t.ID);


        public void SetValue<T>(int index, in T component) where T : struct
        {
            ((ComponentArrayStruct<T>)Storage[typeof(T)])[index] = component;
        }
        public void SetValue<T>(int index, T component) where T : Component
        {
            ((ComponentArray<T>)Storage[typeof(T)])[index] = component;
        }

        public ComponentArrayStruct<T> GetComponentArrayStruct<T>() where T : struct
        {
            return (ComponentArrayStruct<T>)Storage[typeof(T)];
        }
        public ComponentArray<T> GetComponentArray<T>() where T : Component
        {
            return (ComponentArray<T>)Storage[typeof(T)];
        }

        public ComponentArrayBase GetArray<T>() => Storage[typeof(T)];

        public ComponentArrayBase GetComponentArray(Type t)
        {
            return Storage[t];
        }
        public void AddComponent<T>(in T component, long entity) where T : struct
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                ((ComponentArrayStruct<T>)Storage[typeof(T)]).Add(component);
                EntityID.Add(entity);
            }
        }
        public void AddComponent<T>(T component, long entity) where T : Component
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                ((ComponentArray<T>)Storage[typeof(T)]).Add(component);
                EntityID.Add(entity);
            }
        }

        public void RemoveEntity(Entity e) => EntityID.RemoveAt(EntityID.IndexOf(e.Index));
        
        public void SetComponent<T>(int index, in T component) where T : struct
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                ((ComponentArrayStruct<T>)Storage[typeof(T)])[index] = component;
            }
        }
        public void SetComponent<T>(int index, T component) where T : Component
        {
            if(Storage.ContainsKey(typeof(T)))
            {
                ((ComponentArray<T>)Storage[typeof(T)])[index] = component;
            }
        }

        internal void AddEntity(Entity entity) => EntityID.Add(entity.Index);

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("Type : [");
            result.Append(string.Join(";",ID.Types?.Select(x => x.Name).ToList()??new List<string>()));
            result.Append(']');
            result.AppendLine();
            result.Append("Storages : [");
            result.Append(string.Join(";",Storage.Values.ToList().Select(x => x.StringRepresentation())));
            result.Append(']');
            return result.ToString();
        }

        public override bool Equals(object? obj)
        {
            return obj is Archetype archetype &&
                //    EqualityComparer<Dictionary<Type, IComponentArray>>.Default.Equals(Storage, archetype.Storage);
                   EqualityComparer<ArchetypeID>.Default.Equals(ID, archetype.ID);
                //    EqualityComparer<List<IComponentArray>>.Default.Equals(Components, archetype.Components) &&
                //    Length == archetype.Length;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}