using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Storage;

public readonly ref struct EntityIterator(World world)
{
    readonly World world = world;

    public readonly Entity this[Index index]
    {
        get
        {
            int i = 0;
            var real = !index.IsFromEnd ? index.Value : Count() - index.Value;
            foreach (var e in this)
            {
                i += 1;
                if (i == real)
                    return e;
            }
            throw new IndexOutOfRangeException($"{index} out of range for size {i}");
        }
    }

    public readonly int Count()
    {
        int i = 0;
        foreach (var _ in this)
            i += 1;
        return i;
    }



public readonly Enumerator GetEnumerator() => new(world);

public ref struct Enumerator(World world)
{
    World world = world;
    readonly ArchetypeList.Enumerator ArchEnumerator => world.Archetypes.GetEnumerator();
    int archPos = 0;
    bool started = false;
    public readonly Entity Current => new(archPos, ArchEnumerator.Current.Value);

    public bool MoveNext()
    {
        if (!started)
        {
            var next = ArchEnumerator.MoveNext();
            while (next && ArchEnumerator.Current.Value.Length == 0)
            {
                next = ArchEnumerator.MoveNext();
            }
            if (!next)
                return false;
            started = true;
            return true;
        }
        else
        {
            archPos += 1;
            if (archPos > ArchEnumerator.Current.Value.Length)
            {
                var next = ArchEnumerator.MoveNext();
                while (next && ArchEnumerator.Current.Value.Length == 0)
                {
                    next = ArchEnumerator.MoveNext();
                }
                if (!next)
                    return false;
                archPos = 0;
                return true;
            }
            return true;
        }
    }
}
}
