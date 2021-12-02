using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WonkECS
{
    public struct ArchetypeID
    {
        public List<Type>? Types = new();

        public int Count => Types?.Count ?? 0;

        public ArchetypeID(HashSet<Type> types)
        {
            Types= types.ToList();
        }
        public ArchetypeID(IEnumerable<Type> types)
        {
            Types= types.ToHashSet().ToList();
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
            if(other.Types != null && this.Types != null)
                return other.Types.Except(this.Types);
            return new List<Type>();
        }
        public IEnumerable<Type> Intersect(ArchetypeID other)
        {
            if(other.Types != null && this.Types != null)
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

    }
}