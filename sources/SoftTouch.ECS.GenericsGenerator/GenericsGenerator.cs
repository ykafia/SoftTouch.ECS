using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.Text;


namespace SoftTouch.ECS.GenericsGenerator;

[Generator]
public partial class GenericsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // No initialization required for this one
        GenerateQueries(context);
        // GenerateWorldQueries(context);
        GenerateFilters(context);
        //GenerateProcessorSetExtensions(context);
        GenerateProcessorCreators(context);
        GenerateInsertOrSpawn(context);
    }

    public void GenerateProcessorCreators(IncrementalGeneratorInitializationContext context)
    {
        var code = new StringBuilder();
        code
            .AppendLine("using CommunityToolkit.HighPerformance.Buffers;")
            .AppendLine("using SoftTouch.ECS.Processors;")
            .AppendLine("using SoftTouch.ECS.Querying;")
            .AppendLine("using SoftTouch.ECS.Scheduling;")
            .AppendLine("using SoftTouch.ECS.Arrays;")
            .AppendLine("namespace SoftTouch.ECS;")
            .AppendLine("public partial class App")
            .AppendLine("{");

        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .AppendLine($"public App AddProcessors<TStage, {string.Join(", ", range.Select(x => $"P{x}"))}>()")
                .AppendLine($"    where TStage: SubStage");
            foreach (var r in range)
                code.AppendLine($"    where P{r} : Processor, new()");
            code
                .AppendLine("{");
            foreach (var r in range.Select(x => $"P{x}"))
                code.AppendLine($"Schedule.Add<TStage>(new {r}());");
            code
                .AppendLine("return this;")
                .AppendLine("}");
        }
        for (int i = 0; i < 16; i++)
        {
            var range = Enumerable.Range(1, i + 1);
            code
                .AppendLine($"public App AddProcessors<TStage>({string.Join(", ", range.Select(x => $"Processor p{x}"))})")
                .AppendLine("    where TStage: SubStage");
            code
                .AppendLine("{")
                .AppendLine($"Schedule.Add<TStage>([{string.Join(", ", range.Select(x => $"p{x}"))}]);")
                .AppendLine("return this;")
                .AppendLine("}");
        }
        code.AppendLine("}");
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("App.g.cs", code.ToSourceText()));
    }

    public static void GenerateQueries(IncrementalGeneratorInitializationContext context)
    {
        var code = new StringBuilder();
        code.AppendLine("using CommunityToolkit.HighPerformance.Buffers;")
            .AppendLine("using System.Collections.Immutable;")
            .AppendLine("using SoftTouch.ECS.Storage;")
            .AppendLine("using SoftTouch.ECS.Processors;")
            .AppendLine()
            .AppendLine("namespace SoftTouch.ECS.Querying;")
            .AppendLine();


        for (int i = 2; i < 17; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "T" + x);
            code
                .AppendLine($"public delegate void EntityUpdateFunc<{string.Join(", ", generics)}>({string.Join(", ", generics.Select(x => $"ref {x} componen{x.ToLower()}"))})");
            foreach (var g in generics)
                code.AppendLine($"    where {g} : struct");
            code.AppendLine(";");
            code
                .AppendLine($"public delegate void EntityUpdateFuncData<TData, {string.Join(", ", generics)}>(ref TData data, {string.Join(", ", generics.Select(x => $"ref {x} componen{x.ToLower()}"))})");
            foreach (var g in generics)
                code.AppendLine($"    where {g} : struct");
            code.AppendLine(";");
            code
                .AppendLine($"public delegate void EntityUpdateFuncIndexed<{string.Join(", ", generics)}>(Entity index, {string.Join(", ", generics.Select(x => $"ref {x} componen{x.ToLower()}"))})");
            foreach (var g in generics)
                code.AppendLine($"    where {g} : struct");
            code.AppendLine(";");
            code
                .AppendLine($"public delegate void EntityUpdateFuncIndexedData<TData, {string.Join(", ", generics)}>(ref TData data, Entity index, {string.Join(", ", generics.Select(x => $"ref {x} componen{x.ToLower()}"))})");
            foreach (var g in generics)
                code.AppendLine($"    where {g} : struct");
            code.AppendLine(";");
            // code
            //     .AppendLine()
            //     .AppendLine($"public record struct Query<{string.Join(", ", generics)}>() : IEntityQuery")
            //     .AppendLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
            //     .AppendLine("{")
            //     .AppendLine($"public static Type[] Types {{ get; }} = [{string.Join(", ", generics.Select(x => $"typeof({x})"))}];")
            //     .AppendLine("public Type[] ImplTypes => Types;")
            //     .AppendLine("public World World { get; set; }")
            //     .AppendLine("public Processor CallingProcessor { get; init; }")
            //     .AppendLine()
            //     .AppendLine("public bool HasAccessTo<TComponent>()")
            //     .AppendLine($"    =>{string.Join("||", generics.Select(x => $"typeof(TComponent) == typeof({x})"))};")
            //     .AppendLine()
            //     .AppendLine($"public readonly void ForEach(EntityUpdateFunc<{string.Join(", ", generics)}> updater)")
            //     .AppendLine("{")
            //     .AppendLine("foreach(var e in this)")
            //     .AppendLine($"    updater.Invoke({string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
            //     .AppendLine("}")
            //     .AppendLine()
            //     .AppendLine($"public readonly void ForEach<TData>(ref TData data, EntityUpdateFuncData<TData, {string.Join(", ", generics)}> updater)")
            //     .AppendLine("{")
            //     .AppendLine("foreach(var e in this)")
            //     .AppendLine($"    updater.Invoke(ref data, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
            //     .AppendLine("}")
            //     .AppendLine()
            //     .AppendLine($"public readonly void ForEachIndexed(EntityUpdateFuncIndexed<{string.Join(", ", generics)}> updater)")
            //     .AppendLine("{")
            //     .AppendLine("foreach(var e in this)")
            //     .AppendLine($"    updater.Invoke(e.Entity, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
            //     .AppendLine("}")
            //     .AppendLine()
            //     .AppendLine($"public readonly void ForEachIndexedData<TData>(ref TData data, EntityUpdateFuncIndexedData<TData, {string.Join(", ", generics)}> updater)")
            //     .AppendLine("{")
            //     .AppendLine("foreach(var e in this)")
            //     .AppendLine($"    updater.Invoke(ref data, e.Entity, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
            //     .AppendLine("}")
            //     .AppendLine()
            //     .AppendLine($"public readonly WorldQueryEnumerator<Query<{string.Join(", ", generics)}>> GetEnumerator() => new(this);")
            //     .AppendLine("}");

            // code.AppendLine();

            code
                .AppendLine($"public record struct Query<{string.Join(", ", generics)}, TFilter>() : IFilteredEntityQuery")
                .AppendLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .AppendLine("    where TFilter : IFilterQuery, new()")
                .AppendLine("{")
                .AppendLine($"public static Type[] Types {{ get; }} = [{string.Join(", ", generics.Select(x => $"typeof({x})"))}];")
                .AppendLine("public Type[] ImplTypes => Types;")
                .AppendLine("public static IFilterQuery Filters { get; } = new TFilter();")
                .AppendLine("public World World { get; set; }")
                .AppendLine("public Processor CallingProcessor { get; init; }")
                .AppendLine()
                .AppendLine("public bool HasAccessTo<TComponent>()")
                .AppendLine($"    =>{string.Join("||", generics.Select(x => $"typeof(TComponent) == typeof({x})"))}")
                .AppendLine($"        && !Filters.ImplWithoutTypes.Contains(typeof(TComponent));")
                .AppendLine()
                .AppendLine($"public readonly void ForEach(EntityUpdateFunc<{string.Join(", ", generics)}> updater)")
                .AppendLine("{")
                .AppendLine("foreach(var e in this)")
                .AppendLine($"    updater.Invoke({string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
                .AppendLine("}")
                .AppendLine()
                .AppendLine($"public readonly void ForEach<TData>(ref TData data, EntityUpdateFuncData<TData, {string.Join(", ", generics)}> updater)")
                .AppendLine("{")
                .AppendLine("foreach(var e in this)")
                .AppendLine($"    updater.Invoke(ref data, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
                .AppendLine("}")
                .AppendLine()
                .AppendLine($"public readonly void ForEach(EntityUpdateFuncIndexed<{string.Join(", ", generics)}> updater)")
                .AppendLine("{")
                .AppendLine("foreach(var e in this)")
                .AppendLine($"    updater.Invoke(e.Entity, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
                .AppendLine("}")
                .AppendLine()
                .AppendLine($"public readonly void ForEach<TData>(ref TData data, EntityUpdateFuncIndexedData<TData, {string.Join(", ", generics)}> updater)")
                .AppendLine("{")
                .AppendLine("foreach(var e in this)")
                .AppendLine($"    updater.Invoke(ref data, e.Entity, {string.Join(", ", generics.Select(x => $"ref e.Get<{x}>()"))});")
                .AppendLine("}")
                .AppendLine()
                .AppendLine($"public readonly WorldQueryEnumerator<Query<{string.Join(", ", generics)}, TFilter>> GetEnumerator() => new(this);")
                .AppendLine("}");

            code.AppendLine();

        }

        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Queries.g.cs", code.ToSourceText()));

    }


    public static void GenerateFilters(IncrementalGeneratorInitializationContext context)
    {
        var code = new StringBuilder();
        code.AppendLine("using CommunityToolkit.HighPerformance.Buffers;")
            .AppendLine("using System.Collections.Immutable;")
            .AppendLine()
            .AppendLine("namespace SoftTouch.ECS.Querying;")
            .AppendLine();
        for (int i = 2; i < 17; i++)
        {

            var generics = Enumerable.Range(1, i).Select(x => "T" + x);
            code
                .AppendLine($"public record With<{string.Join(", ", generics)}>() : IFilter")
                .AppendLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .AppendLine("{")
                .AppendLine($"public static TypeSet WithTypes {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .AppendLine($"public static TypeSet WithoutTypes {{ get; }} =  TypeSet.Empty;")
                .AppendLine("public TypeSet ImplWithTypes => WithTypes;")
                .AppendLine("public TypeSet ImplWithoutTypes => WithoutTypes;")
                .AppendLine("}");

            code.AppendLine();

            code
                .AppendLine($"public record Without<{string.Join(", ", generics)}>() : IFilter")
                .AppendLine(string.Join("\n", generics.Select(x => $"    where {x} : struct")))
                .AppendLine("{")
                .AppendLine($"public static TypeSet WithoutTypes {{ get; }} = TypeSet.Create({string.Join(", ", generics.Select(x => $"typeof({x})"))});")
                .AppendLine($"public static TypeSet WithTypes {{ get; }} = TypeSet.Empty;")
                .AppendLine("public TypeSet ImplWithTypes => WithTypes;")
                .AppendLine("public TypeSet ImplWithoutTypes => WithoutTypes;")
                .AppendLine("}");
        }

        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Filters.g.cs", code.ToSourceText()));

    }
}


public static class SourceCodeExtensions
{
    public static SourceText ToSourceText(this StringBuilder code)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code.ToString());
        var root = syntaxTree.GetRoot().NormalizeWhitespace();
        return SourceText.From(root.ToFullString(), Encoding.UTF8);
    }
}