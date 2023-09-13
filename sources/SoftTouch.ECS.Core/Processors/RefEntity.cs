using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;

public ref struct RefEntity<T1>
    where T1 : struct, IEquatable<T1>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(out T1 comp1)
    {
        comp1 = world[id].Get<T1>();
    }
}

public ref struct RefEntity<T1, T2>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
    }
}

public ref struct RefEntity<T1, T2, T3>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
    }
}

public ref struct RefEntity<T1, T2, T3, T4>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
    where T4 : struct, IEquatable<T4>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3,
            out T4 comp4
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
        comp4 = world[id].Get<T4>();
    }
}

public ref struct RefEntity<T1, T2, T3, T4, T5>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
    where T4 : struct, IEquatable<T4>
    where T5 : struct, IEquatable<T5>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3,
            out T4 comp4,
            out T5 comp5
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
        comp4 = world[id].Get<T4>();
        comp5 = world[id].Get<T5>();
    }
}
public ref struct RefEntity<T1, T2, T3, T4, T5, T6>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
    where T4 : struct, IEquatable<T4>
    where T5 : struct, IEquatable<T5>
    where T6 : struct, IEquatable<T6>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3,
            out T4 comp4,
            out T5 comp5,
            out T6 comp6
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
        comp4 = world[id].Get<T4>();
        comp5 = world[id].Get<T5>();
        comp6 = world[id].Get<T6>();
    }
}
public ref struct RefEntity<T1, T2, T3, T4, T5, T6, T7>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
    where T4 : struct, IEquatable<T4>
    where T5 : struct, IEquatable<T5>
    where T6 : struct, IEquatable<T6>
    where T7 : struct, IEquatable<T7>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3,
            out T4 comp4,
            out T5 comp5,
            out T6 comp6,
            out T7 comp7
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
        comp4 = world[id].Get<T4>();
        comp5 = world[id].Get<T5>();
        comp6 = world[id].Get<T6>();
        comp7 = world[id].Get<T7>();
    }
}

public ref struct RefEntity<T1, T2, T3, T4, T5, T6, T7, T8>
    where T1 : struct, IEquatable<T1>
    where T2 : struct, IEquatable<T2>
    where T3 : struct, IEquatable<T3>
    where T4 : struct, IEquatable<T4>
    where T5 : struct, IEquatable<T5>
    where T6 : struct, IEquatable<T6>
    where T7 : struct, IEquatable<T7>
    where T8 : struct, IEquatable<T8>
{
    int id;
    World world;

    public RefEntity(int id, World world)
    {
        this.id = id;
        this.world = world;
    }

    public void Set<T>(in T component) where T : struct, IEquatable<T>
        => world[id].Set(in component);

    public T Get<T>() where T : struct, IEquatable<T>
        => world[id].Get<T>();

    public void Deconstruct(
            out T1 comp1,
            out T2 comp2,
            out T3 comp3,
            out T4 comp4,
            out T5 comp5,
            out T6 comp6,
            out T7 comp7,
            out T8 comp8
        )
    {
        comp1 = world[id].Get<T1>();
        comp2 = world[id].Get<T2>();
        comp3 = world[id].Get<T3>();
        comp4 = world[id].Get<T4>();
        comp5 = world[id].Get<T5>();
        comp6 = world[id].Get<T6>();
        comp7 = world[id].Get<T7>();
        comp8 = world[id].Get<T8>();
    }
}
