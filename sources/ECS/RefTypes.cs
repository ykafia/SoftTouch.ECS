using ECSharp.Arrays;

namespace ECSharp;

public struct Ref<T> where T : struct
{
    public ArchetypeRecord Index {get; private set;}
    ComponentList<T> components;
    Span<T> span => components.AsSpan();

    public Ref(ComponentList<T> comps, ArchetypeRecord index)
    {
        components = comps;
        Index = index;
    }

    public void Set(T c) => span[Index.ArchetypeIndex] = c;
    public T Get(out T c) => c = span[Index.ArchetypeIndex];
    public static implicit operator T(Ref<T> r) => r.span[r.Index.ArchetypeIndex];

}