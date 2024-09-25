using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static System.MemoryExtensions;
using System.Diagnostics;
using System.Collections.Immutable;

namespace SoftTouch.ECS.Storage;

[DebuggerDisplay("{TypesToString}")]
public ref struct RefArchetypeID
{
    public Span<Type> Types { get; }
    public readonly int Count => Types.Length;

    public RefArchetypeID(Span<Type> types)
    {
        Types = types;
        Types.Sort(Types, (a, b) => string.Compare(a.FullName, b.FullName));
    }

    // public static implicit operator ArchetypeID(TemporaryArchetypeID tid) => new(tid);
    public static implicit operator RefArchetypeID(Span<Type> types) => new(types);

    internal readonly bool IsAddedType(ArchetypeID other)
        => Types.Length == other.Types.Length + 1;

    public readonly bool Contains(Type value)
    {
        foreach (var type in Types)
            if (type == value)
                return true;
        return false;
    }

    public readonly bool IsSupersetOf(in RefArchetypeID other)
    {
        if (Types.Length < other.Types.Length)
            return false;
        for (int i = 0; i < Types.Length; i += 1)
            if (Types[i] != other.Types[i])
                return false;
        return true;
    }
    public readonly bool IsStrictSupersetOf(in RefArchetypeID other)
    {
        if (Types.Length <= other.Types.Length)
            return false;
        for (int i = 0; i < Types.Length; i += 1)
            if (Types[i] != other.Types[i])
                return false;
        return true;
    }

    public readonly bool IsSubsetOf(in RefArchetypeID other)
        => other.IsSupersetOf(in this);
    public readonly bool IsStrictSubsetOf(in RefArchetypeID other)
        => other.IsStrictSupersetOf(in this);


    public static bool operator ==(RefArchetypeID id1, ArchetypeID id2) => id1.Equals(id2);
    public static bool operator !=(RefArchetypeID id1, ArchetypeID id2) => !id1.Equals(id2);

    public static bool operator ==(RefArchetypeID id1, RefArchetypeID id2) => id1.IsSupersetOf(id2) && id1.IsSubsetOf(id2);
    public static bool operator !=(RefArchetypeID id1, RefArchetypeID id2) => !(id1.IsSupersetOf(id2) && id1.IsSubsetOf(id2));

    public override readonly bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is ArchetypeID id)
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

    public override readonly int GetHashCode()
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
}