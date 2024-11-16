using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static System.MemoryExtensions;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace SoftTouch.ECS.Storage;


public static class ArchetypeIDBuilder
{
    public static ArchetypeID Create(ReadOnlySpan<Type> items) => new(items);
}


[DebuggerDisplay("{TypesToString}")]
[CollectionBuilder(typeof(ArchetypeIDBuilder), "Create")]
public readonly struct ArchetypeID
{
    public Type[] Types { get; }
    public int Count => Types.Length;
    public Span<Type> Span => Types.AsSpan();

    public ArchetypeID(ReadOnlySpan<Type> types)
    {
        Types = types.ToArray();
        Array.Sort(Types, (a, b) => string.Compare(a.FullName, b.FullName));
    }
    public ArchetypeID(ReadOnlySpan<ComponentBox> components)
    {
        Types = new Type[components.Length];
        for(int i = 0; i < components.Length; i++)
            Types[i] = components[i].ComponentType;
        Array.Sort(Types, (a, b) => string.Compare(a.FullName, b.FullName));
    }

    public ArchetypeID(Type[] types)
    {
        Types = types;
        Array.Sort(Types, (a, b) => string.Compare(a.FullName, b.FullName));
    }


    // public static implicit operator ArchetypeID(TemporaryArchetypeID tid) => new(tid);
    public static implicit operator ArchetypeID(Type[] types) => new(types);
    public static implicit operator ArchetypeID(ReadOnlySpan<Type> types) => new(types);

    internal bool IsAddedType(ArchetypeID other)
        => Types.Length == other.Types.Length + 1;

    public bool Contains(Type t) => Types.Contains(t);

    public bool IsSupersetOf(in ArchetypeID other)
    {
        if (Types.Length < other.Types.Length)
            return false;
        for (int i = 0; i < Types.Length; i += 1)
            if (Types[i] != other.Types[i])
                return false;
        return true;
    }
    public readonly bool IsStrictSupersetOf(in ArchetypeID other)
    {
        if(Types.Length <= other.Types.Length)
            return false;
        for (int i = 0; i < Types.Length; i += 1)
            if (Types[i] != other.Types[i])
                return false;
        return true;
    }

    public bool IsSubsetOf(in ArchetypeID other)
        => other.IsSupersetOf(in this);
    public bool IsStrictSubsetOf(in ArchetypeID other)
        => other.IsStrictSupersetOf(in this);


    public static bool operator ==(ArchetypeID id1, ArchetypeID id2) => id1.Equals(id2);
    public static bool operator !=(ArchetypeID id1, ArchetypeID id2) => !id1.Equals(id2);
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if(obj is ArchetypeID id)
        {
            if (Types.Length != id.Types.Length)
                return false;

            for (int i = 0; i < Types.Length; i++)
            {
                if (Types[i] != id.Types[i])
                    return false;
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        if (Types == null) return 0;

        unchecked
        {
            int hash = 17;
            for (int i = 0; i < Types.Length; i++)
                hash *= 31 + (Types[i].FullName ?? Types[i].Name).GetHashCode();
            return hash;
        }
    }

    public Span<Type>.Enumerator GetEnumerator() => Types.AsSpan().GetEnumerator();
}