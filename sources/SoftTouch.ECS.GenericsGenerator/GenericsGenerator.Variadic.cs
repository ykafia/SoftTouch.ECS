using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SoftTouch.ECS.GenericsGenerator;


public partial class GenericsGenerator
{
    // public void GenerateVariadicQueries(IncrementalGeneratorInitializationContext context)
    // {
    //     var classDeclarations = context.SyntaxProvider
    //        .CreateSyntaxProvider(
    //            predicate: static (s, _) => IsClassDeclarationWithBaseList(s),
    //            transform: static (ctx, _) => GetClassAndUsings(ctx))
    //        .Where(static item => item != null);

    //     context.RegisterSourceOutput(classDeclarations, (spc, item) =>
    //     {
    //         if (item != null)
    //         {
    //             var (classDeclaration, usings, queries) = item.Value;
    //             if (usings.Any(u => u.Name!.ToString() == "SoftTouch.ECS.Processors"))
    //             {
    //             }
    //         }
    //     });
    // }

    // private string GenerateDoSomethingMethod(int count)
    // {
    //     var sb = new StringBuilder();
    //     sb.AppendLine("namespace SoftTouch.ECS;");
    //     sb.AppendLine("public static partial class VariadicClass");
    //     sb.AppendLine("{");

    //     sb.Append("public static void DoSomething<");

    //     // Generate generic type parameters
    //     for (int i = 1; i <= count; i++)
    //     {
    //         sb.Append($"T{i}");
    //         if (i < count)
    //             sb.Append(", ");
    //     }

    //     sb.Append(">(");

    //     // Generate method parameters
    //     for (int i = 1; i <= count; i++)
    //     {
    //         sb.Append($"T{i} value{i}");
    //         if (i < count)
    //             sb.Append(", ");
    //     }

    //     sb.AppendLine(")");
    //     sb.AppendLine("{");
    //     sb.AppendLine("    // Method body");
    //     sb.AppendLine("    return;");
    //     sb.AppendLine("}");

    //     sb.AppendLine("}");
    //     return sb.ToString();
    // }

    private static bool IsClassDeclarationWithBaseList(SyntaxNode node)
    {
        return node is ClassDeclarationSyntax classDeclaration && classDeclaration.BaseList != null;
    }

    private static ProcessorInformation? GetClassAndUsings(GeneratorSyntaxContext context)
    {
        var classDeclaration = (ClassDeclarationSyntax)context.Node;
        var usings = classDeclaration.SyntaxTree.GetRoot().DescendantNodes().OfType<UsingDirectiveSyntax>().ToImmutableArray();

        var processorType = classDeclaration.BaseList!.Types
            .FirstOrDefault(t => t.Type is SimpleNameSyntax name && name.Identifier.Text == "Processor");

        if (processorType != null)
        {
            var typeArguments = processorType.Type is GenericNameSyntax genericName
                ? [.. genericName.TypeArgumentList.Arguments]
                : ImmutableArray<TypeSyntax>.Empty;

            return (classDeclaration, usings, typeArguments);
        }

        return null;
    }
}

internal record struct ProcessorInformation(ClassDeclarationSyntax Item1, ImmutableArray<UsingDirectiveSyntax> Item2, ImmutableArray<TypeSyntax> Item3)
{
    public static implicit operator (ClassDeclarationSyntax, ImmutableArray<UsingDirectiveSyntax>, ImmutableArray<TypeSyntax>)(ProcessorInformation value)
    {
        return (value.Item1, value.Item2, value.Item3);
    }

    public static implicit operator ProcessorInformation((ClassDeclarationSyntax, ImmutableArray<UsingDirectiveSyntax>, ImmutableArray<TypeSyntax>) value)
    {
        return new ProcessorInformation(value.Item1, value.Item2, value.Item3);
    }
}