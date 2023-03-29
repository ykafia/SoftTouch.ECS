using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static System.MemoryExtensions;

namespace SoftTouch.ECS.Storage;

public struct ArchetypeID : IComparable
{
    static int globalId = 0;
    static int GetNext() => ++globalId;

    public readonly int Id;

    public readonly Type[] Types;
    public readonly Span<Type> Span => Types.AsSpan();

    public int Count => Types.Length;
    public ArchetypeID(params Type[] types)
    {
        Types = types;
        Id = GetNext();
    }
    
    // public ArchetypeID(IEnumerable<Type> types)
    // {
    //     Types = types.ToHashSet();
    //     Id = GetNext();
    // }

    public bool IsStrictSupersetOf(ArchetypeID other)
    {
        if(Count <= other.Count) return false;
        for(int i = 0; i< Types.Length; i++) {
            if(!other.Types.Contains(Types[i]))
                return false;
        }
        return true;
    }
    public bool IsSupersetOf(ArchetypeID other)
    {
        if(Count < other.Count) return false;
        for(int i = 0; i< Types.Length; i++) {
            if(!other.Types.Contains(Types[i]))
                return false;
        }
        return true;
    }
    public bool IsAddedType(ArchetypeID other) => IsStrictSupersetOf(other) && Count == other.Count + 1;
    public bool IsStrictSubsetOf(ArchetypeID other)
    {
        if(other.Count <= Count) return false;
        for(int i = 0; i< other.Types.Length; i++) {
            if(!Types.Contains(other.Types[i]))
                return false;
        }
        return true;
    }
    public bool IsRemovedType(ArchetypeID other) => IsStrictSubsetOf(other) && Count == other.Count - 1;

    public void Except(ArchetypeID other, out Type[] types)
    {
        if (other.Types != null && Types != null && Types.Length > other.Types.Length)
        {
            var size = Types.Length - other.Types.Length;   
            var result = new Type[size];
            var id = 0;
            for(int i = 0; i < Types.Length; i++){
                if(!other.Types.Contains(Types[i]))
                {
                    result[id] = Types[i];
                }
            }
            types = result;
        }
        else 
            types = Array.Empty<Type>();
    }
    public void Intersect(ArchetypeID other, out Type[] types)
    {
        if (other.Types != null && Types != null)
        {
            types = other.Types.Intersect(Types).ToArray();
        }
        else 
            types = Array.Empty<Type>();
    }

    public override bool Equals(object? obj)
    {
        return obj is ArchetypeID iD &&
               Id == iD.Id &&
               EqualityComparer<Type[]>.Default.Equals(Types, iD.Types) &&
               Count == iD.Count;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Types, Count);
    }

    public int CompareTo(object? obj)
    {
        if(obj is ArchetypeID aid)
            return aid.Id.CompareTo(Id);
        else 
            throw new NotImplementedException();
    }

    internal bool IsSupersetOf(Span<Type> types)
    {
        if (Count <= types.Length) return false;
        for (int i = 0; i < types.Length; i++)
        {
            if (!Types.Contains(types[i]))
                return false;
        }
        return true;
    }

    internal bool IsStrictSubsetOf(Span<Type> types)
    {
        if (types.Length <= Count) return false;
        for (int i = 0; i < Count; i++)
        {
            var contains = false;
            for (int j = 0; j < types.Length; j++)
                if (types[j] == Types[i])
                    contains = false;
            if (!contains) return false;
        }
        return true;
    }

    public static bool operator ==(ArchetypeID left, ArchetypeID right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ArchetypeID left, ArchetypeID right)
    {
        return !(left == right);
    }
}