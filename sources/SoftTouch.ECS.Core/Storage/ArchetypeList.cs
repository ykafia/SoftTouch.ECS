namespace SoftTouch.ECS.Storage;



public record struct ArchetypeItem(ArchetypeID Key, Archetype Value);

public class ArchetypeList
{

    public ArchetypeListKeys Keys => new ArchetypeListKeys(this);
    public ArchetypeListValues Values => new ArchetypeListValues(this);
    public int Count => list.Count;

    List<ArchetypeItem> list = new();
    Dictionary<ArchetypeID, int> lookup = new();

    public Archetype this[ArchetypeID id]
    {
        get => list[lookup[id]].Value;
        set
        {
            if(ContainsKey(id))
            {
                list[lookup[id]] = list[lookup[id]] with { Value = value };
            }
            else
            {
                list.Add(new(id, value));
                lookup.Add(id, list.Count - 1);
            }
        }
    }

    public bool TryGetValue(ArchetypeID id, out Archetype arch)
    {
        var result = lookup.TryGetValue(id, out var index);
        if (!result)
        {
            arch = null!;
            return false;
        }
        else
        {
            arch = list[index].Value;
            return true;
        }
    }


    public bool ContainsKey(ArchetypeID Key)
        => lookup.ContainsKey(Key);

    public void Add(ArchetypeID key, Archetype value)
    {
        list.Add(new(key, value));
        lookup.Add(key, list.Count - 1);
    }

    public Enumerator GetEnumerator() => new(this);

    public ref struct Enumerator
    {
        List<ArchetypeItem>.Enumerator enumerator;
        public Enumerator(ArchetypeList list)
        {
            enumerator = list.list.GetEnumerator();
        }
        public ArchetypeItem Current => enumerator.Current;
        public bool MoveNext() => enumerator.MoveNext();
    }

    public ref struct ArchetypeListKeys
    {
        ArchetypeList list;

        public ArchetypeListKeys(ArchetypeList list)
        {
            this.list = list;
        }

        public Enumerator GetEnumerator() => new(list);

        public ref struct Enumerator
        {
            List<ArchetypeItem>.Enumerator enumerator;
            public Enumerator(ArchetypeList list)
            {
                this.enumerator = list.list.GetEnumerator();
            }
            public ArchetypeID Current => enumerator.Current.Key;
            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

        }
    }
    public ref struct ArchetypeListValues
    {
        ArchetypeList list;

        public Archetype this[int index] => list.list[index].Value;

        public ArchetypeListValues(ArchetypeList list)
        {
            this.list = list;
        }
        public Enumerator GetEnumerator() => new(list);

        public ref struct Enumerator
        {
            List<ArchetypeItem>.Enumerator enumerator;
            public Enumerator(ArchetypeList list)
            {
                this.enumerator = list.list.GetEnumerator();
            }
            public Archetype Current => enumerator.Current.Value;
            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

        }
    }
}