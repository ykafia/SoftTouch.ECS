using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Querying;

public sealed class TypeSet()
{

    public static TypeSet Empty { get; } = new();

    public static TypeSet Create(params Type[] types)
    {
        var result = new TypeSet();
        foreach(var t in types)
            result.Add(t);
        return result;
    }

    readonly HashSet<Type> set = [];
    readonly List<Type> list = [];

    public int Count => list.Count;

  
    internal void Add(Type type)
    {
        if(!set.Contains(type))
        {
            list.Add(type);
            set.Add(type);
        }
    }
    internal void Remove(Type type)
    {
        if (set.Contains(type))
        {
            list.Remove(type);
            set.Remove(type);
        }
    }

    public bool Contains(Type t)
        => set.Contains(t);

    public bool IsQuerySubsetOf(Type[] elements)
    {
        foreach (var t in this)
            if (!elements.Contains(t))
                return false;
        return true;
    }

    public List<Type>.Enumerator GetEnumerator() => list.GetEnumerator();
}
