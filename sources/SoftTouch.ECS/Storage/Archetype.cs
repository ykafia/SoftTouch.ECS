using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{
    static object myLocker = new object();
    public static Archetype CreateEmpty(World w) => new(new List<ComponentBase>(),w);

    public World World { get; init; }
    public Dictionary<Type, ComponentList> Storage = new();
    public EntityLookup EntityLookup { get; init; }
    public bool HasEntities => EntityLookup.Count > 0;

    public ArchetypeID ID = new();

    public ArchetypeEdges Edges = new();

    public int Length => EntityLookup.Count;

    public Archetype(IEnumerable<ComponentBase> components, World w)
    {
        foreach(var c in components)
        {
            Storage[c.GetComponentType()] = c.EmptyArray();
        }
        ID = new ArchetypeID(components.Select(x => x.GetComponentType()).ToArray());
        World = w;
        EntityLookup = new();
    }

    public Archetype(IEnumerable<ComponentList> componentArrays, World w)
    {
        foreach(var ca in componentArrays)
        {
            Storage[ca.ComponentType] = ca.New();
        };
        ID = new ArchetypeID(componentArrays.Select(x => x.ComponentType).ToArray());
        World = w;
        EntityLookup = new();
    }

    public Entity this[int i]
    {
        get => World[EntityLookup[i]];
    }


    private Span<T> GetComponentSpan<T>() where T : struct
    {
        return ((ComponentList<T>)Storage[typeof(T)]).AsSpan();
    }
    internal ComponentList<T> GetComponentArray<T>() where T : struct
    {
        return (ComponentList<T>)Storage[typeof(T)];
    }
    public void GetComponent<T>(int i, out T c) where T : struct
    {
        c = ((ComponentList<T>)Storage[typeof(T)])[i];
    }

    internal void SetComponent<T>(in T component, long entity) where T : struct
    {
        if(Storage.ContainsKey(typeof(T)))
        {
            var array = GetComponentArray<T>();
            if (EntityLookup.TryGetValue(entity, out var idx))
            {
                array[idx] = component;
            }
            else
            {
                array.Add(component);
                EntityLookup[entity] = array.Count - 1;
            }
        }
    }

    public void RemoveEntity(long idx)
    {
        if(EntityLookup.Count > 0) 
            EntityLookup.Remove(idx);
    }
    public ComponentList<T> GetComponentList<T>() where T : struct
    {
        return (ComponentList<T>)Storage[typeof(T)];
    }
    public void SetComponent<T>(int index, in T component) where T : struct
    {
        lock (myLocker)
        {
            if (Storage.ContainsKey(typeof(T)))
            {
                GetComponentSpan<T>()[index] = component;
            }
        }
    }

    internal void AddEntity(EntityId idx) => EntityLookup.Add(idx);

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