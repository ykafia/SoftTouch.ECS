using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Processors;



public abstract class Processor
{
    public World World { get; set; }

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
}


public abstract class Processor<Q> : Processor
    where Q : struct, IWorldQuery
{

    public Q Query => new() { World = World };
    protected Processor(World world) : base(world)
    {
    }
}

public abstract class Processor<Q1, Q2> : Processor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{

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

    public Q1 Query1 => new() { World = World };
    public Q2 Query2 => new() { World = World };
    public Q3 Query3 => new() { World = World };
    public Q4 Query4 => new() { World = World };

    protected Processor(World world) : base(world)
    {
    }


}