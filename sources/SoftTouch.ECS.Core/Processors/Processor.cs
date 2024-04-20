using SoftTouch.ECS.Events;
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

    internal World World { get; set; }
    public static ImmutableList<Type> StaticEventReaders { get; internal set; } = [];
    public static ImmutableList<Type> StaticRelatedTypes { get; internal set; } = [];
    public ImmutableList<Type> RelatedTypes => StaticRelatedTypes;
    public ImmutableList<Type> RelatedEvents => StaticEventReaders;

    public Processor()
    {
        World = null!;
    }

    public Processor(World world)
    {
        World = world;
    }
    public abstract void Update();

    public static Processor Create<T>(World world)
        where T : Processor, new()
    {
        return new T() { World = world };
    }


    public static Processor From<Q1>(UpdaterFunc<Q1> func) where Q1 : struct, IWorldQuery
        => new DelegateProcessor<Q1>(func);
    public static Processor From<Q1, Q2>(UpdaterFunc<Q1, Q2> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        => new DelegateProcessor<Q1, Q2>(func);
    public static Processor From<Q1, Q2, Q3>(UpdaterFunc<Q1, Q2, Q3> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        => new DelegateProcessor<Q1, Q2, Q3>(func);
    public static Processor From<Q1, Q2, Q3, Q4>(UpdaterFunc<Q1, Q2, Q3, Q4> func)
        where Q1 : struct, IWorldQuery
        where Q2 : struct, IWorldQuery
        where Q3 : struct, IWorldQuery
        where Q4 : struct, IWorldQuery
        => new DelegateProcessor<Q1, Q2, Q3, Q4>(func);
    
    public static Processor From<T>() where T : Processor, new()
        => new T();

}


public abstract class Processor<Q>(World world) : Processor(world), IProcessorRelation
    where Q : struct, IWorldQuery
{

    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q();
        if (q1 is IEntityQuery eq1)
        {
            foreach (var t in eq1.ImplTypes)
                hash.Add(t);
        }
        if(q1 is IEventWriter evq)
            StaticEventReaders.Add(evq.EventDataType);

        StaticRelatedTypes = [.. hash];
    }
    public Q Query => new() { World = World };
}

public abstract class Processor<Q1, Q2>(World world) : Processor(world)
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    static Processor()
    {
        var hash = new HashSet<Type>();
        var q1 = new Q1();
        var q2 = new Q2();
        if (q1 is IEntityQuery eq1)
        {
            foreach (var t in eq1.ImplTypes)
                hash.Add(t);
        }
        if (q2 is IEntityQuery eq2)
        {
            foreach (var t in eq2.ImplTypes)
                hash.Add(t);
        }
        if(q1 is IEventWriter evq)
            StaticEventReaders.Add(evq.EventDataType);
        if(q2 is IEventWriter evq2)
            StaticEventReaders.Add(evq2.EventDataType);
        StaticRelatedTypes = [.. hash];
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
}

public abstract class Processor<Q1, Q2, Q3>(World world) : Processor(world)
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
        {
            foreach (var t in eq1.ImplTypes)
                hash.Add(t);
        }
        if (q2 is IEntityQuery eq2)
        {
            foreach (var t in eq2.ImplTypes)
                hash.Add(t);
        }
        if (q3 is IEntityQuery eq3)
        {
            foreach (var t in eq3.ImplTypes)
                hash.Add(t);
        }
        if(q1 is IEventWriter evq)
            StaticEventReaders.Add(evq.EventDataType);
        if(q2 is IEventWriter evq2)
            StaticEventReaders.Add(evq2.EventDataType);
        if(q3 is IEventWriter evq3)
            StaticEventReaders.Add(evq3.EventDataType);
        StaticRelatedTypes = [.. hash];
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
    public Q3 Query3 => new() { World = World };
}


public abstract class Processor<Q1, Q2, Q3, Q4>(World world) : Processor(world)
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
        {
            foreach (var t in eq1.ImplTypes)
                hash.Add(t);
        }
        if (q2 is IEntityQuery eq2)
        {
            foreach (var t in eq2.ImplTypes)
                hash.Add(t);
        }
        if (q3 is IEntityQuery eq3)
        {
            foreach (var t in eq3.ImplTypes)
                hash.Add(t);
        }
        if (q4 is IEntityQuery eq4)
        {
            foreach (var t in eq4.ImplTypes)
                hash.Add(t);
        }
        if(q1 is IEventWriter evq)
            StaticEventReaders.Add(evq.EventDataType);
        if(q2 is IEventWriter evq2)
            StaticEventReaders.Add(evq2.EventDataType);
        if(q3 is IEventWriter evq3)
            StaticEventReaders.Add(evq3.EventDataType);
        if(q4 is IEventWriter evq4)
            StaticEventReaders.Add(evq4.EventDataType);
        StaticRelatedTypes = [.. hash];
    }

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
    public Q3 Query3 => new() { World = World };
    public Q4 Query4 => new() { World = World };
}