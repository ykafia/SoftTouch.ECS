using Microsoft.CodeAnalysis;
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
        GenerateWorldQueries(context);
        GenerateFilters(context);
    }




    public static void GenerateQueries(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
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
                .WriteLine($"public static Type[] TypesRead {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .WriteLine($"public static Type[] TypesWrite {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayWrite {{ get; }} = Array.Empty<Type>();")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct Write<{string.Join(", ", generics)}>() : IWriteComponent")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static Type[] TypesRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesWrite {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .WriteLine($"public static Type[] TypesMayRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayWrite {{ get; }} = Array.Empty<Type>();")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct MayRead<{string.Join(", ", generics)}>() : IMayReadComponent")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static Type[] TypesRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesWrite {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayRead {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .WriteLine($"public static Type[] TypesMayWrite {{ get; }} = Array.Empty<Type>();")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct MayWrite<{string.Join(", ", generics)}>() : IMayWriteComponent")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static Type[] TypesRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesWrite {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayRead {{ get; }} = Array.Empty<Type>();")
                .WriteLine($"public static Type[] TypesMayWrite {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .CloseAllBlocks();
        }

        context.AddSource("Queries.g.cs", code.ToString());

    }

    public static void GenerateWorldQueries(GeneratorExecutionContext context)
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
                        .WriteEmptyLines(2)
                        .WriteLine("namespace SoftTouch.ECS.Querying;")
                        .WriteEmptyLines(3);
        for (int i = 2; i < 5; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "TComp" + x);
            code
                .WriteLine($"public record struct Query<{string.Join(", ", generics)}>() : IWorldQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : IComponentQuery, new()")))
                .OpenBlock()
                .WriteLine("public static IReadComponent Read { get; }")
                .WriteLine("public static IMayReadComponent MayRead { get; }")
                .WriteLine("public static IWriteComponent Write { get; }")
                .WriteLine("public static IMayWriteComponent MayWrite { get; }")
                .WriteEmptyLines(2)
                .WriteLine("static Query()")
                .OpenBlock();

            foreach (var t in generics)
            {
                code.WriteLine($"var comp{t} = new {t}();")
                    .WriteLine($"if (comp{t} is IReadComponent read{t})")
                    .WriteLine($"    Read = read{t};")
                    .WriteLine($"else if (comp{t} is IMayReadComponent mayread{t})")
                    .WriteLine($"    MayRead = mayread{t};")
                    .WriteLine($"else if (comp{t} is IWriteComponent write{t})")
                    .WriteLine($"    Write = write{t};")
                    .WriteLine($"else if (comp{t} is IMayWriteComponent mayWrite{t})")
                    .WriteLine($"    MayWrite = mayWrite{t};");

            }
            code.CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record struct FilteredQuery<{string.Join(", ", generics)}, TFilter>() : IFilteredWorldQuery")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : IComponentQuery, new()")))
                .WriteLine("    where TFilter : IFilterQuery, new()")
                .OpenBlock()
                .WriteLine("public static IReadComponent Read { get; }")
                .WriteLine("public static IMayReadComponent MayRead { get; }")
                .WriteLine("public static IWriteComponent Write { get; }")
                .WriteLine("public static IMayWriteComponent MayWrite { get; }")
                .WriteLine("public static IFilterQuery Filters { get; }")
                .WriteEmptyLines(2)
                .WriteLine("static FilteredQuery()")
                .OpenBlock();

            foreach (var t in generics)
            {
                code.WriteLine($"var comp{t} = new {t}();")
                    .WriteLine($"if (comp{t} is IReadComponent read{t})")
                    .WriteLine($"    Read = read{t};")
                    .WriteLine($"else if (comp{t} is IMayReadComponent mayread{t})")
                    .WriteLine($"    MayRead = mayread{t};")
                    .WriteLine($"else if (comp{t} is IWriteComponent write{t})")
                    .WriteLine($"    Write = write{t};")
                    .WriteLine($"else if (comp{t} is IMayWriteComponent mayWrite{t})")
                    .WriteLine($"    MayWrite = mayWrite{t};")
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
                .WriteLine($"public static Type[] WithTypes {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .WriteLine($"public static Type[] WithoutTypes {{ get; }} =  Array.Empty<Type>();")
                .CloseAllBlocks();

            code.WriteEmptyLines(3);

            code
                .WriteLine($"public record Without<{string.Join(", ", generics )}>() : IFilter")
                .WriteLine(string.Join("\n", generics.Select(x => $"    where {x} : struct, IEquatable<{x}>")))
                .OpenBlock()
                .WriteLine($"public static Type[] WithoutTypes {{ get; }} = {{ {string.Join(", ", generics.Select(x => $"typeof({x})"))} }};")
                .WriteLine($"public static Type[] WithTypes {{ get; }} = Array.Empty<Type>();")
                .CloseAllBlocks();
        }

        context.AddSource("Filters.g.cs", code.ToString());

    }

}