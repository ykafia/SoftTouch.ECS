﻿using Microsoft.CodeAnalysis;
using SoftTouch.ECS.Generators.Shared;
using System.ComponentModel;


namespace SoftTouch.ECS.GenericsGenerator;

[Generator]
public class GenericsGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // No initialization required for this one
    }
    public void Execute(GeneratorExecutionContext context)
    {
        GenerateQueries(context);
        // GenerateWorldQueries(context);
        GenerateFilters(context);
        //GenerateProcessorSetExtensions(context);
        GenerateProcessorCreators(context);
    }

    public void GenerateProcessorCreators(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code
            .WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
            .WriteLine("using SoftTouch.ECS.Processors;")
            .WriteLine("using SoftTouch.ECS.Querying;")
            .WriteLine("using SoftTouch.ECS.Scheduling;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS;")
            .WriteEmptyLines(3)
            .WriteLine("public partial class App")
            .OpenBlock();

        for(int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .WriteLine($"public App AddProcessors<{string.Join(", ", range.Select(x => $"P{x}"))}>(string name)");
            foreach(var r in range)
                code.WriteLine($"    where P{r} : Processor, new()");
            code
                .OpenBlock()
                .WriteLine("using var merge = new MergeStage")
                .OpenBlock()
                .WriteLine("Name = name,")
                .WriteLine($"Processors = MemoryOwner<Processor>.Allocate({i+1}, AllocationMode.Clear)")
                .CloseBlockWith(";");
            foreach(var r in range)
                code.WriteLine($"merge.Processors.Span[{r-1}] = new P{r}(){{World = World}};");
            code
                .WriteLine("Schedule.Add(merge);")
                .WriteLine("return this;")
                .CloseBlock();
        }
        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .WriteLine($"public App AddProcessors(string name, {string.Join(", ", range.Select(x => $"Processor p{x}"))})");
            code
                .OpenBlock()
                .WriteLine("using var merge = new MergeStage")
                .OpenBlock()
                .WriteLine("Name = name,")
                .WriteLine($"Processors = MemoryOwner<Processor>.Allocate({i + 1}, AllocationMode.Clear)")
                .CloseBlockWith(";");
            foreach (var r in range)
            {
                code.WriteLine($"p{r}.World = World;");
                code.WriteLine($"merge.Processors.Span[{r - 1}] = p{r};");
            }
            code
                .WriteLine("Schedule.Add(merge);")
                .WriteLine("return this;")
                .CloseBlock();
        }
        code.CloseAllBlocks();
        context.AddSource("App.g.cs",code.ToString());
    }

    private void GenerateProcessorSetExtensions(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code
            .WriteLine("using SoftTouch.ECS.Processors;")
            .WriteLine("using SoftTouch.ECS.Querying;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS.Scheduling;")
            .WriteEmptyLines(3)
            .WriteLine("public static partial class ProcessorSetExtensions")
            .OpenBlock();

    }

    public static void GenerateQueries(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
            .WriteLine("using System.Collections.Immutable;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS.Querying;")
            .WriteEmptyLines(3);
        for (int i = 2; i < 17; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "T" + x);
            code
                .WriteLine($"public record struct Read<{string.Join(", ", generics)}>() : IReadComponent")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static ImmutableSortedSet<Type> TypesRead {{ get; }} = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)), {string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine($"public static ImmutableSortedSet<Type> TypesWrite {{ get; }} = ImmutableSortedSet<Type>.Empty;")
                .WriteLine("public ImmutableSortedSet<Type> ImplRead => TypesRead;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWrite => TypesWrite;")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct Write<{string.Join(", ", generics)}>() : IWriteComponent")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static ImmutableSortedSet<Type> TypesRead {{ get; }} = ImmutableSortedSet<Type>.Empty;")
                .WriteLine($"public static ImmutableSortedSet<Type> TypesWrite {{ get; }} = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)), {string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine("public ImmutableSortedSet<Type> ImplRead => TypesRead;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWrite => TypesWrite;")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

        }

        context.AddSource("Queries.g.cs", code.ToString());

    }

    public static void GenerateWorldQueries(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
            .WriteLine("using System.Collections.Immutable;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS.Querying;")
            .WriteEmptyLines(3);
        for (int i = 2; i < 5; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "TComp" + x);
            code
                .WriteLine($"public record struct Query<{string.Join(", ", generics)}>() : IEntityQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : IComponentQuery, new()")))
                .OpenBlock()
                .WriteLine("public static IReadComponent Read { get; }")
                .WriteLine("public static IWriteComponent Write { get; }")
                .WriteLine("public World World { get; set; }")
                .WriteLine("public ImmutableSortedSet<Type> ImplRead => Read == null ?  ImmutableSortedSet<Type>.Empty : Read.ImplRead;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWrite => Write == null ?  ImmutableSortedSet<Type>.Empty : Write.ImplWrite;")
                .WriteLine($"public WorldQueryEnumerator<Query<{string.Join(", ", generics)}>> GetEnumerator() => new(this);")
                .WriteLine("public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));")
                .WriteLine("public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null &&ImplWrite.Contains(t));")
                .WriteLine("public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));")
                .WriteLine("public bool CanWrite(Type t) => ImplWrite.Contains(t);")
                .WriteEmptyLines(2)
                .WriteLine("static Query()")
                .OpenBlock();

            foreach (var t in generics)
            {
                code.WriteLine($"var comp{t} = new {t}();")
                    .WriteLine($"if (comp{t} is IReadComponent read{t})")
                    .WriteLine($"    Read = read{t};")
                    .WriteLine($"else if (comp{t} is IWriteComponent write{t})")
                    .WriteLine($"    Write = write{t};");

            }
            code.CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct FilteredQuery<{string.Join(", ", generics)}, TFilter>() : IFilteredEntityQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : IComponentQuery, new()")))
                .WriteLine("    where TFilter : IFilterQuery, new()")
                .OpenBlock()
                .WriteLine("public static IReadComponent Read { get; }")
                .WriteLine("public static IWriteComponent Write { get; }")
                .WriteLine("public static IFilterQuery Filters { get; }")
                .WriteLine("public World World { get; set; }")
                .WriteLine("public ImmutableSortedSet<Type> ImplRead => Read == null ?  ImmutableSortedSet<Type>.Empty : Read.ImplRead;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWrite => Write == null ?  ImmutableSortedSet<Type>.Empty : Write.ImplWrite;")
                .WriteLine($"public WorldFilteredQueryEnumerator<FilteredQuery<{string.Join(", ", generics)}, TFilter>> GetEnumerator() => new(this);")
                .WriteLine("public bool CanRead<T>() where T : struct, IEquatable<T> => CanRead(typeof(T));")
                .WriteLine("public bool CanRead(Type t) => (Read != null && ImplRead.Contains(t)) || (Write != null &&ImplWrite.Contains(t));")
                .WriteLine("public bool CanWrite<T>() where T : struct, IEquatable<T> => ImplWrite.Contains(typeof(T));")
                .WriteLine("public bool CanWrite(Type t) => ImplWrite.Contains(t);")
                .WriteEmptyLines(2)
                .WriteLine("static FilteredQuery()")
                .OpenBlock();

            foreach (var t in generics)
            {
                code.WriteLine($"var comp{t} = new {t}();")
                    .WriteLine($"if (comp{t} is IReadComponent read{t})")
                    .WriteLine($"    Read = read{t};")
                    .WriteLine($"else if (comp{t} is IWriteComponent write{t})")
                    .WriteLine($"    Write = write{t};")
                    .WriteEmptyLines(1);

            }
            code.WriteLine("Filters = new TFilter();")
                .CloseAllBlocks();
        }

        context.AddSource("WorldQueries.g.cs", code.ToString());

    }

    public static void GenerateFilters(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
            .WriteLine("using System.Collections.Immutable;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS.Querying;")
            .WriteEmptyLines(3);
        for (int i = 2; i < 17; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "T" + x);
            code
                .WriteLine($"public record Has<{string.Join(", ", generics)}>() : IFilter")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static ImmutableSortedSet<Type> WithTypes {{ get; }} = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)), {string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine($"public static ImmutableSortedSet<Type> WithoutTypes {{ get; }} =  ImmutableSortedSet<Type>.Empty;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWithTypes => WithTypes;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWithoutTypes => WithoutTypes;")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record Without<{string.Join(", ", generics )}>() : IFilter")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static ImmutableSortedSet<Type> WithoutTypes {{ get; }} = ImmutableSortedSet.Create(Comparer<Type>.Create(static (a,b) => a.Name.CompareTo(b.Name)), {string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine($"public static ImmutableSortedSet<Type> WithTypes {{ get; }} = ImmutableSortedSet<Type>.Empty;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWithTypes => WithTypes;")
                .WriteLine("public ImmutableSortedSet<Type> ImplWithoutTypes => WithoutTypes;")
                .CloseAllBlocks();
        }

        context.AddSource("Filters.g.cs", code.ToString());

    }

}