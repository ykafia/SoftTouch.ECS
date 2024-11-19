using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using SoftTouch.ECS.Arrays;
using System.Runtime.InteropServices;

namespace SoftTouch.ECS.Storage;

public partial class Archetype
{
    public static Archetype CreateEmpty(World w) => new([], w);

    public World World { get; init; }
    public Dictionary<Type, ComponentArray> Storage = [];
    public List<int> EntityLookup { get; internal set; }
    public bool HasEntities => EntityLookup.Count > 0;

    public ArchetypeID ID;

    public int Length => EntityLookup.Count;

    public ComponentArray this[Type type]
    {
        get => Storage[type];
        set => Storage[type] = value;
    }

    public Archetype(ArchetypeID aid, ReusableList<ComponentBox> components, World w)
    {
        foreach (var c in components.Span)
        {
            Storage[c.ComponentType] = c.ToArray();
        }
        ID = aid;
        World = w;
        EntityLookup = [];
        components.Dispose();
    }
    public Archetype(ArchetypeID aid, ReadOnlySpan<ComponentBox> components, World w)
    {
        foreach (var c in components)
        {
            Storage[c.ComponentType] = c.ToArray();
        }
        ID = aid;
        World = w;
        EntityLookup = [];
    }

    public bool Has<T>() where T : struct => Has(typeof(T));
    public bool Has(Type type) => Storage.ContainsKey(type);


    public Archetype(ReadOnlySpan<ComponentArray> componentArrays, World w)
    {
        using var tmp = new ReusableList<Type>();
        foreach (var ca in componentArrays)
        {
            Storage[ca.ComponentType] = ca.Create();
            tmp.Add(ca.ComponentType);
        };
        ID = new ArchetypeID(tmp.Span);
        World = w;
        EntityLookup = [];
    }

    public int this[in Entity i] => EntityLookup[i];

    internal ComponentArray<T> GetComponentArray<T>() where T : struct
    {
        return (ComponentArray<T>)Storage[typeof(T)];
    }
    public void GetComponent<T>(int i, out T c) where T : struct
    {
        c = GetComponentArray<T>()[i];
    }

    internal void SetEntityComponent<T>(in Entity entity, in T component) where T : struct
    {
        var array = GetComponentArray<T>();
        var idx = EntityLookup.IndexOf(entity);
        if (idx > -1)
        {
            array[idx] = component;
        }
        else
        {
            array.Add(component);
            EntityLookup.Add(entity);
        }
    }

    internal void RemoveEntity(in Entity entity)
    {
        if (EntityLookup.Count > 0)
        {
            var idx = EntityLookup.IndexOf(entity);
            if(idx > -1)
            {
                foreach(var t in ID.Types)
                    Storage[t].RemoveAt(idx);
                EntityLookup.RemoveAt(idx);
            }
        }
    }
    public void SetComponent<T>(int index, in T component) where T : struct
    {
        GetComponentArray<T>()[index] = component;
    }
    public void SetComponent(int index, ComponentBox component)
    {
        if(Storage.TryGetValue(component.ComponentType, out var components))
            components.TryAdd(component);
    }

    internal void AddEntity(int idx)
    {
        EntityLookup.Add(idx);
    }

    public bool GetComponent(Entity entity, Type type, out ComponentBox box)
    {
        var idx = Lookup(entity);
        if(idx > 0 && Storage.ContainsKey(type))
        {
            box = Storage[type].GetComponent(idx);
            return true;
        }
        box = null!;
        return false;
    }

    public int Lookup(Entity entity)
        => EntityLookup.IndexOf(entity);
    public override string ToString()
    {
        var result =
        new StringBuilder()
            .Append("Type : [")
            .Append(string.Join(";", Storage.Keys.Select(x => x.Name) ?? []))
            .Append(']')
            .Append(" Storages : [")
            .Append(string.Join(";", Storage.Values.Select(x => x.ToString())))
            .Append(']');
        return result.ToString();
    }

    internal void Clear()
    {
        foreach(var t in ID.Span)
        {
            Storage[t].Clear();
        }
        EntityLookup.Clear();
    }

    public override int GetHashCode()
    {
        return ID.GetHashCode();
    }

    public Archetype Clone()
    {
        var clone = new Archetype
        {
            ID = ID
        };
        foreach (var t in ID.Types)
            clone.Storage[t] = Storage[t].Clone();
        clone.EntityLookup = new(EntityLookup);
        return clone;
    }

    
}