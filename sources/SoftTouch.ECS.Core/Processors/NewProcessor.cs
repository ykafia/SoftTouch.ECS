using SoftTouch.ECS.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.NewProcessors;

public class ProcessorProvider
{
    public static ProcessorProvider Instance { get; } = new();


    private readonly Dictionary<Type, NewProcessor> processors;

    private ProcessorProvider() 
    { 
        processors = new();
    }


    public static T Get<T>() where T : NewProcessor
    {
        return Instance.processors[typeof(T)] as T ?? throw new Exception("Cast failed");
    }
    public static bool TryGet<T>(out T processor) where T : NewProcessor
    {
        processor = null!;
        var result = Instance.processors.TryGetValue(typeof(T), out var p);
        if(result)
            processor = p as T ?? throw new Exception("Cast failed");
        return result;
    }

    public static void Register<T>(T processor)
        where T : NewProcessor
    {
        Instance.processors.Add(typeof(T), processor);
    }
}


public interface IProcessorCreator<T>
    where T : NewProcessor
{
    public abstract static T CreateNewProcessor(World world);
}

public abstract class NewProcessor
{
    public World World { get; set; }

    public NewProcessor(World world)
    {
        World = world;
    }
    public abstract void Update(); 
}


public abstract class NewProcessor<Q> : NewProcessor
    where Q : struct, IWorldQuery
{
    public Q Query { get; }
    protected NewProcessor(World world) : base(world)
    {
        Query = new() { World = world };
    }
}

public abstract class NewProcessor<Q1, Q2> : NewProcessor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
{
    public Q1 Query1 { get; }
    public Q2 Query2 { get; }

    protected NewProcessor(World world) : base(world)
    {
        Query1 = new() { World = world };
        Query2 = new() { World = world };
    }

   
}

public abstract class NewProcessor<Q1, Q2, Q3> : NewProcessor
    where Q1 : struct, IWorldQuery
    where Q2 : struct, IWorldQuery
    where Q3 : struct, IWorldQuery
{
    public Q1 Query1 { get; }
    public Q2 Query2 { get; }
    public Q3 Query3 { get; }

    protected NewProcessor(World world) : base(world)
    {
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
    public Q1 Query1 { get; }
    public Q2 Query2 { get; }
    public Q3 Query3 { get; }
    public Q4 Query4 { get; }

    protected NewProcessor(World world) : base(world)
    {
        Query1 = new() { World = world };
        Query2 = new() { World = world };
        Query3 = new() { World = world };
        Query4 = new() { World = world };
    }

    
}



public class TestProcessor : NewProcessor<FilteredQuery<Read<int, float>, Write<double>, Filter<Without<bool>>>>
{
    public TestProcessor(World world) : base(world)
    {
    }

    public override void Update()
    {
    }
}