using System.Collections.Immutable;

namespace SoftTouch.ECS.Querying;


public static class QueryExtensions
{
    public static bool IsQuerySubsetOf(this ImmutableSortedSet<Type> sortedSet, Type[] elements)
    {
        foreach (var t in sortedSet)
            if (!elements.Contains(t))
                return false;
        return true;
    }
}