using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.NewProcessors;



public abstract class NewProcessor
{
    public abstract World World { get; set; }

    public NewProcessor()
    {
        World = null!;
    }

    public NewProcessor(World world)
    {
        World = world;
    }
    public abstract void Update();

    public static NewProcessor Create<T>(World world)
        where T : NewProcessor, new()
    {
        return new T() { World = world };
    }
}


public abstract class NewProcessor<Q> : NewProcessor
    where Q : struct, IWorldQuery
{
    private World world;
    public override World World { get => world; set { world = value; Query = new() { World = value }; } }
    public Q Query { get; private set; }
    protected NewProcessor(World world) : base(world)
    {
        this.world = world;
        Query = new() { World = world };
    }
}

public abstract class NewProcessor<Q1, Q2> : NewProcessor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    private World world;
    public override World World 
    { 
        get => world; 
        set 
        { 
            world = value; 
            Query1 = new() { World = value }; 
            Query2 = new() { World = value };
        } 
    }

    public Q1 Query1 { get; private set;}
    public Q2 Query2 { get; private set; }

    protected NewProcessor(World world) : base(world)
    {
        this.world = world;
        Query1 = new() { World = world };
        Query2 = new() { World = world };
    }

   
}

public abstract class NewProcessor<Q1, Q2, Q3> : NewProcessor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
{

    private World world;
    public override World World
    {
        get => world;
        set
        {
            world = value;
            Query1 = new() { World = value };
            Query2 = new() { World = value };
            Query3 = new() { World = value };
        }
    }

    public Q1 Query1 { get; private set;}
    public Q2 Query2 { get; private set;}
    public Q3 Query3 { get; private set; }

    protected NewProcessor(World world) : base(world)
    {
        this.world= world;
        Query1 = new() { World = world };
        Query2 = new() { World = world };
        Query3 = new() { World = world };
    }

    
}


public abstract class NewProcessor<Q1, Q2, Q3, Q4> : NewProcessor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
    where Q4 : struct, IWorldQuery
{
    private World world;
    public override World World
    {
        get => world;
        set
        {
            world = value;
            Query1 = new() { World = value };
            Query2 = new() { World = value };
            Query3 = new() { World = value };
            Query4 = new() { World = value };
        }
    }

    public Q1 Query1 { get; private set;}
    public Q2 Query2 { get; private set;}
    public Q3 Query3 { get; private set;}
    public Q4 Query4 { get; private set; }

    protected NewProcessor(World world) : base(world)
    {
        this.world = world;
        Query1 = new() { World = world };
        Query2 = new() { World = world };
        Query3 = new() { World = world };
        Query4 = new() { World = world };
    }

    
}



public class TestProcessor : NewProcessor<FilteredQuery<Read<int, float>, Write<double>, Filter<Without<bool>>>>
{
    public TestProcessor() : base(null!)
    {
    }
    public TestProcessor(World world) : base(world)
    {
    }

    public override void Update()
    {
    }

    public static void Something()
    {
        var processor = Create<TestProcessor>(new World());
    }
}