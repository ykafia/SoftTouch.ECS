using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using SoftTouch.ECS.ComponentData;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{
    object myLocker = new object();
    public static Archetype CreateEmpty(World w) => new(new List<ComponentBase>(), w);

    public World World { get; init; }
    public Dictionary<Type, ComponentArray> Storage = new();
    public EntityLookup EntityLookup { get; init; }
    public bool HasEntities => EntityLookup.Count > 0;

    public ArchetypeID ID = new();

    public ArchetypeEdges Edges = new();

    public int Length => EntityLookup.Count;

    public Archetype(IEnumerable<ComponentBase> components, World w)
    {
        foreach (var c in components)
        {
            Storage[c.GetComponentType()] = c.EmptyArray();
        }
        ID = new ArchetypeID(components.Select(x => x.GetComponentType()).ToArray());
        World = w;
        EntityLookup = new();
    }

    public Archetype(IEnumerable<ComponentArray> componentArrays, World w)
    {
        foreach (var ca in componentArrays)
        {
            Storage[ca.ComponentType] = ca.Create();
        };
        ID = new ArchetypeID(componentArrays.Select(x => x.ComponentType).ToArray());
        World = w;
        EntityLookup = new();
    }

    public int this[EntityId i]
    {
        get
        {
            EntityLookup.TryGetValue(in i, out var res);
            return res;
        }
    }


    private Span<T> GetComponentSpan<T>() where T : struct, IEquatable<T>
    {
        return ((ComponentArray<T>)Storage[typeof(T)]).Span;
    }
    internal ComponentArray<T> GetComponentArray<T>() where T : struct, IEquatable<T>
    {
        return (ComponentArray<T>)Storage[typeof(T)];
    }
    public void GetComponent<T>(int i, out T c) where T : struct, IEquatable<T>
    {
        c = ((ComponentArray<T>)Storage[typeof(T)])[i];
    }

    internal void SetEntityComponent<T>(in EntityId entity, in T component) where T : struct, IEquatable<T>
    {
        lock (myLocker)
        {
            var array = GetComponentArray<T>();
            if (EntityLookup.TryGetValue(entity, out var idx))
            {
                array[idx] = component;
            }
            else
            {
                array.Add(component);
                EntityLookup.Set(World[entity].Index, array.Count - 1);
            }
        }
    }

    public void RemoveEntity(EntityId idx)
    {
        if (EntityLookup.Count > 0)
            EntityLookup.Remove(idx);
    }
    public void SetComponent<T>(int index, in T component) where T : struct, IEquatable<T>
    {
        lock (myLocker)
        {
            GetComponentArray<T>()[index] = component;
        }
    }

    internal void AddEntity(EntityId idx) => EntityLookup.Add(idx);

    public override string ToString()
    {
        var result =
        new StringBuilder()
            .Append("Type : [")
            .Append(string.Join(";", Storage.Keys.Select(x => x.Name) ?? new List<string>()))
            .Append(']')
            .AppendLine()
            .Append("Storages : [")
            .Append(string.Join(";", Storage.Values.Select(x => x.ToString())))
            .Append(']');
        return result.ToString();
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }
}