using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ECSharp
{
    public struct ArchetypeID
    {
        static int globalId = 0;
        static int GetNext() => ++globalId;

        public readonly int Id;

        public readonly List<Type> Types = new();

        public int Count => Types?.Count ?? 0;
        public ArchetypeID(params Type[] types)
        {
            Types = types.Distinct().ToList();
            Id = GetNext();
        }
        public ArchetypeID(HashSet<Type> types)
        {
            Types = types.ToList();
            Id = GetNext();
        }
        public ArchetypeID(IEnumerable<Type> types)
        {
            Types = types.ToList();
            Id = GetNext();
        }

        public bool IsSupersetOf(ArchetypeID other)
        {
            return
                other.Types != null &&
                this.Types?.Intersect(other.Types).Count() == other.Count;

        }
        public bool IsAddedType(ArchetypeID other) => IsSupersetOf(other) && this.Count == other.Count + 1;
        public bool IsSubsetOf(ArchetypeID other)
        {
            return
                other.Types != null &&
                this.Types?.Intersect(other.Types).Count() == this.Count;
        }
        public bool IsRemovedType(ArchetypeID other) => IsSubsetOf(other) && this.Count == other.Count - 1;

        public IEnumerable<Type> Except(ArchetypeID other)
        {
            if (other.Types != null && this.Types != null)
                return other.Types.Except(this.Types);
            return new List<Type>();
        }
        public IEnumerable<Type> Intersect(ArchetypeID other)
        {
            if (other.Types != null && this.Types != null)
                return other.Types.Intersect(this.Types);
            return new List<Type>();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is ArchetypeID id &&
                id.Types != null &&
                this.Types != null &&
                id.Types.Count == this.Types.Count &&
                id.Types.Intersect(this.Types).Count() == this.Count;
        }

        public override int GetHashCode()
        {
            return Types?.Select(x => x.GetHashCode()).Sum() ?? 0;
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