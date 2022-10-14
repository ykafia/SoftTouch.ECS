using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ECSharp
{
    public struct ArchetypeID : IComparable
    {
        static int globalId = 0;
        static int GetNext() => ++globalId;

        public readonly int Id;

        public readonly HashSet<Type> Types = new();

        public int Count => Types?.Count ?? 0;
        public ArchetypeID(params Type[] types)
        {
            Types = types.Distinct().ToHashSet();
            Id = GetNext();
        }
        public ArchetypeID(HashSet<Type> types)
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
            return Types.IsSupersetOf(other.Types) && !Types.IsSubsetOf(other.Types);
        }
        public bool IsSupersetOf(ArchetypeID other)
        {
            return Types.IsSupersetOf(other.Types);
        }
        public bool IsAddedType(ArchetypeID other) => IsStrictSupersetOf(other) && Count == other.Count + 1;
        public bool IsStrictSubsetOf(ArchetypeID other)
        {
            return Types.IsSubsetOf(other.Types) && !Types.IsSupersetOf(other.Types);
        }
        public bool IsRemovedType(ArchetypeID other) => IsStrictSubsetOf(other) && Count == other.Count - 1;

        public IEnumerable<Type> Except(ArchetypeID other)
        {
            if (other.Types != null && Types != null)
                return other.Types.Except(Types);
            return Enumerable.Empty<Type>();
        }
        public IEnumerable<Type> Intersect(ArchetypeID other)
        {
            if (other.Types != null && Types != null)
                return other.Types.Intersect(Types);
            return Enumerable.Empty<Type>();
        }

        public override bool Equals(object? obj)
        {
            return obj is ArchetypeID iD &&
                   Id == iD.Id &&
                   EqualityComparer<HashSet<Type>>.Default.Equals(Types, iD.Types) &&
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

        public static bool operator ==(ArchetypeID left, ArchetypeID right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ArchetypeID left, ArchetypeID right)
        {
            return !(left == right);
        }
    }
}