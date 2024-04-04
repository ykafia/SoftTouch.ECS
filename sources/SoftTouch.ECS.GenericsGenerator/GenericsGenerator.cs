using Microsoft.CodeAnalysis;
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
            .WriteLine("using SoftTouch.ECS.Arrays;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS;")
            .WriteEmptyLines(3)
            .WriteLine("public partial class App")
            .OpenBlock();

        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .WriteLine($"public App AddProcessors<TStage, {string.Join(", ", range.Select(x => $"P{x}"))}>()")
                .WriteLine($"    where TStage : Stage, new()");
            foreach (var r in range)
                code.WriteLine($"    where P{r} : Processor, new()");
            code
                .OpenBlock()
                .WriteLine("using var merge = new MergeStage<TStage>")
                .OpenBlock()
                .WriteLine($"Processors = [{string.Join(", ", range.Select(r => $"new P{ r }(){{World = World}}"))}]")
                .CloseBlockWith(";");
            code
                .WriteLine("Schedule.Add(merge);")
                .WriteLine("return this;")
                .CloseBlock();
        }
        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .WriteLine($"public App AddProcessors<TStage>({string.Join(", ", range.Select(x => $"Processor p{x}"))})")
                .WriteLine("    where TStage : Stage, new()");
            code
                .OpenBlock()
                .WriteLine("using var merge = new MergeStage<TStage>")
                .OpenBlock()
                .WriteLine($"Processors = [{string.Join(", ",range.Select(r => $"p{r}"))}]")
                .CloseBlockWith(";");
            
            code.WriteLine("foreach(var e in merge.Processors.Span)")
                .WriteLine("    e.World = World;");

            code
                .WriteLine("Schedule.Add(merge);")
                .WriteLine("return this;")
                .CloseBlock();
        }
        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .WriteLine($"public App SetStages<{string.Join(", ", range.Select(x => $"TStage{x}"))}>()");
            foreach(var e in range)
                code.WriteLine($"    where TStage{e} : Stage, new()");
            code
                .OpenBlock()
                .WriteLine($"using ReusableList<Stage> merge = [{string.Join(", ", range.Select(r => $"new TStage{r}()"))}];")
                .WriteLine("return SetStages(merge);")
                .CloseBlock();
        }
        code.CloseAllBlocks();
        context.AddSource("App.g.cs", code.ToString());
    }

    public static void GenerateQueries(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
            .WriteLine("using System.Collections.Immutable;")
            .WriteEmptyLines(2)
            .WriteLine("namespace SoftTouch.ECS.Querying;")
            .WriteEmptyLines(3);


        // for (int i = 2; i < 17; i++)
        // {

        //     var generics = Enumerable.Range(1, i).Select(x => "T" + x);
        //     code
        //         .WriteLine($"public record struct Read<{string.Join(", ", generics)}>() : IReadComponent")
        //         .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
        //         .OpenBlock()
        //         .WriteLine($"public static TypeSet TypesRead {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
        //         .WriteLine($"public static TypeSet TypesWrite {{ get; }} = TypeSet.Empty;")
        //         .WriteLine("public TypeSet ImplRead => TypesRead;")
        //         .WriteLine("public TypeSet ImplWrite => TypesWrite;")
        //         .CloseAllBlocks();

        //     code.WriteEmptyLines(3);

        //     code
        //         .WriteLine($"public record struct Write<{string.Join(", ", generics)}>() : IWriteComponent")
        //         .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
        //         .OpenBlock()
        //         .WriteLine($"public static TypeSet TypesRead {{ get; }} = TypeSet.Empty;")
        //         .WriteLine($"public static TypeSet TypesWrite {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
        //         .WriteLine("public TypeSet ImplRead => TypesRead;")
        //         .WriteLine("public TypeSet ImplWrite => TypesWrite;")
        //         .CloseAllBlocks();

        //     code.WriteEmptyLines(3);

        // }

        // context.AddSource("Queries.g.cs", code.ToString());


        for (int i = 2; i < 17; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "T" + x);
            code
                .WriteLine($"public record struct Query<{string.Join(", ", generics)}>() : IEntityQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .OpenBlock()
                .WriteLine($"public static Type[] Types {{ get; }} = [{string.Join(", ", generics.Select(x => $"typeof({x})"))}];")
                .WriteLine("public Type[] ImplTypes => Types;")
                .WriteLine("public World World { get; set; }")
                .WriteEmptyLines(1)
                .WriteLine("public bool HasAccessTo<TComponent>()")
                .WriteLine($"    =>{string.Join("||", generics.Select(x => $"typeof(TComponent) == typeof({x})"))};")
                .WriteEmptyLines(1)
                .WriteLine($"public WorldQueryEnumerator<Query<{string.Join(", ", generics)}>> GetEnumerator() => new(this);")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct FilteredQuery<{string.Join(", ", generics)}, TFilter>() : IFilteredEntityQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .WriteLine("    where TFilter : IFilterQuery, new()")
                .OpenBlock()
                .WriteLine($"public static Type[] Types {{ get; }} = [{string.Join(", ", generics.Select(x => $"typeof({x})"))}];")
                .WriteLine("public Type[] ImplTypes => Types;")
                .WriteLine("public static IFilterQuery Filters { get; } = new TFilter();")
                .WriteLine("public World World { get; set; }")
                .WriteEmptyLines(1)
                .WriteLine("public bool HasAccessTo<TComponent>()")
                .WriteLine($"    =>{string.Join("||", generics.Select(x => $"typeof(TComponent) == typeof({x})"))}")
                .WriteLine($"        && !Filters.ImplWithoutTypes.Contains(typeof(TComponent));")
                .WriteEmptyLines(1)
                .WriteLine($"public WorldFilteredQueryEnumerator<FilteredQuery<{string.Join(", ", generics)}, TFilter>> GetEnumerator() => new(this);")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

        }

        context.AddSource("Queries.g.cs", code.ToString());

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
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .OpenBlock()
                .WriteLine($"public static TypeSet WithTypes {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine($"public static TypeSet WithoutTypes {{ get; }} =  TypeSet.Empty;")
                .WriteLine("public TypeSet ImplWithTypes => WithTypes;")
                .WriteLine("public TypeSet ImplWithoutTypes => WithoutTypes;")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record Without<{string.Join(", ", generics)}>() : IFilter")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .OpenBlock()
                .WriteLine($"public static TypeSet WithoutTypes {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .WriteLine($"public static TypeSet WithTypes {{ get; }} = TypeSet.Empty;")
                .WriteLine("public TypeSet ImplWithTypes => WithTypes;")
                .WriteLine("public TypeSet ImplWithoutTypes => WithoutTypes;")
                .CloseAllBlocks();
        }

        context.AddSource("Filters.g.cs", code.ToString());

    }

}