using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;


public interface IProcessorRelation
{
    public abstract static ImmutableList<Type> StaticRelatedTypes { get; }
}

public abstract class Processor : IProcessorRelation
{

    public World World { get; set; }
    public bool Enabled { get; set; }
    public bool RunAndDisable { get; set; }
    public static ImmutableList<Type> StaticRelatedTypes { get; internal set; } = ImmutableList.Create<Type>();
    public ImmutableList<Type> RelatedTypes => StaticRelatedTypes;

    public Processor()
    {
        World = null!;
        Enabled = true;
    }

    public Processor(World world)
    {
        World = world;
        Enabled = true;
    }
    public abstract void Update();

    public static Processor Create<T>(World world)
        where T : Processor, new()
    {
        return new T() { World = world };
    }
}


public abstract class Processor<Q> : Processor, IProcessorRelation
    where Q : struct, IWorldQuery
{

    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q();
        if (q1 is IEntityQuery eq1)
            foreach (var t in eq1.ImplWrite.Concat(eq1.ImplRead))
                hash.Add(t);
        StaticRelatedTypes = hash.ToImmutableList();
    }
    public Q Query => new() { World = World };
    protected Processor(World world) : base(world)
    {
    }
}

public abstract class Processor<Q1, Q2> : Processor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q1();
        var q2 = new Q2();
        if (q1 is IEntityQuery eq1)
            foreach (var t in eq1.ImplWrite.Concat(eq1.ImplRead))
                hash.Add(t);
        if (q2 is IEntityQuery eq2)
            foreach (var t in eq2.ImplWrite.Concat(eq2.ImplRead))
                hash.Add(t);
        StaticRelatedTypes = hash.ToImmutableList();
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };

    protected Processor(World world) : base(world)
    {
    }


}

public abstract class Processor<Q1, Q2, Q3> : Processor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
{
    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q1();
        var q2 = new Q2();
        var q3 = new Q3();
        if (q1 is IEntityQuery eq1)
            foreach (var t in eq1.ImplWrite.Concat(eq1.ImplRead))
                hash.Add(t);
        if (q2 is IEntityQuery eq2)
            foreach (var t in eq2.ImplWrite.Concat(eq2.ImplRead))
                hash.Add(t);
        if (q3 is IEntityQuery eq3)
            foreach (var t in eq3.ImplWrite.Concat(eq3.ImplRead))
                hash.Add(t);
        StaticRelatedTypes = hash.ToImmutableList();
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
    public Q3 Query3 => new() { World = World };

    protected Processor(World world) : base(world)
    {
    }
}


public abstract class Processor<Q1, Q2, Q3, Q4> : Processor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
    where Q4 : struct, IWorldQuery
{
    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q1();
        var q2 = new Q2();
        var q3 = new Q3();
        var q4 = new Q4();
        if (q1 is IEntityQuery eq1)
            foreach (var t in eq1.ImplWrite.Concat(eq1.ImplRead))
                hash.Add(t);
        if (q2 is IEntityQuery eq2)
            foreach (var t in eq2.ImplWrite.Concat(eq2.ImplRead))
                hash.Add(t);
        if (q3 is IEntityQuery eq3)
            foreach (var t in eq3.ImplWrite.Concat(eq3.ImplRead))
                hash.Add(t);
        if (q4 is IEntityQuery eq4)
            foreach (var t in eq4.ImplWrite.Concat(eq4.ImplRead))
                hash.Add(t);
        StaticRelatedTypes = hash.ToImmutableList();
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
    public Q3 Query3 => new() { World = World };
    public Q4 Query4 => new() { World = World };

    protected Processor(World world) : base(world)
    {
    }
}