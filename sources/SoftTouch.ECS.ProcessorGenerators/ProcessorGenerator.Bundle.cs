using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using SoftTouch.ECS.Generators.Shared;

namespace SoftTouch.ECS.ProcessorGenerators;


public class MethodsWithAttributesSR : ISyntaxReceiver
{
    public List<MethodDeclarationSyntax> CandidateMethods { get; } = [];

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (
            syntaxNode is MethodDeclarationSyntax methodDeclarationSyntax
            && methodDeclarationSyntax.AttributeLists.Count > 0)
        {
            CandidateMethods.Add(methodDeclarationSyntax);
        }
    }
}
public partial class ProcessorGenerator : ISourceGenerator
{
    public static void GenerateBundles(GeneratorExecutionContext context)
    {
        var projectAssembly = context.Compilation.Assembly;
        var rootNodes = context.Compilation.SyntaxTrees.Select(tree => tree.GetRoot());
        var semantics = context.Compilation.SyntaxTrees.Select(tree => context.Compilation.GetSemanticModel(tree));
        var code = new CodeWriter();

        Dictionary<string, CodeWriter> dict = [];
        code.WriteLine("using SoftTouch.ECS;")
            .WriteLine("using SoftTouch.ECS.Processors;")
            .WriteLine("using SoftTouch.ECS.Querying;")
            .WriteLine("using SoftTouch.ECS.Scheduling;")
            .WriteLine($"namespace {projectAssembly.Name}.Bundles;");

        if (context.SyntaxReceiver is MethodsWithAttributesSR mw)
            ProcessMethod(context, mw, code, dict);
        foreach (var kv in dict)
            code.WriteEmptyLines(2).Append(kv.Value.WriteLine("return app;").CloseAllBlocks().ToString());
        code.CloseAllBlocks();
        context.AddSource("Bundles.g.cs", code.ToString());
    }

    public static void ProcessMethod(GeneratorExecutionContext context, MethodsWithAttributesSR mw, CodeWriter code, Dictionary<string, CodeWriter> dict)
    {
        foreach (var m in mw.CandidateMethods)
        {
            var model = context.Compilation.GetSemanticModel(m.SyntaxTree);
            if
            (
                model.GetDeclaredSymbol(m) is IMethodSymbol symbol
                && symbol.GetAttributes().Any(ad => ad.AttributeClass?.Name == "BundleAttribute")
            )
            {
                if (symbol.Parameters.All(pr => pr.Type.AllInterfaces.Any(itf => itf.ToDisplayString().Contains("SoftTouch.ECS.Querying.IWorldQuery"))) && symbol.Parameters.All(pr => pr.Type.TypeKind == TypeKind.Structure || pr.Type.TypeKind == TypeKind.Struct))
                {
                    var name = symbol.GetAttributes().First().ConstructorArguments.First().Value?.ToString();
                    if (!string.IsNullOrEmpty(name))
                    {
                        code.WriteLine($"// {name} Step 3");
                        // code.WriteLine($"// {string.Join(", ", symbol.Parameters.Select(pr => string.Join(", ", pr.Type.AllInterfaces.Select(i => i.ToDisplayString()))))} ");

                        CodeWriter? subCode = null!;
                        if (!dict.TryGetValue(name!, out subCode))
                        {
                            subCode = new();
                            subCode
                                .WriteLine($"public struct {name}Bundle : IProcessorBundle")
                                .OpenBlock()
                                .WriteLine("public App AddBundleElements(App app)")
                                .OpenBlock();
                            dict[name!] = subCode;
                        }
                        subCode?
                            .WriteLine($"app.AddProcessor<Main>(Processor.From<{string.Join(", ", m.ParameterList.Parameters.Select(p => $"{p.Type}"))}>({symbol.ContainingType.ToDisplayString()}.{m.Identifier}));");
                    }
                }
                else
                {
                    context.ReportDiagnostic(
                        Diagnostic.Create(
                            new DiagnosticDescriptor(id: "STCHERR01", title: "SoftTouch diagnostics", category: "SoftTouch-Generator", defaultSeverity: DiagnosticSeverity.Error, isEnabledByDefault: true, messageFormat: $"Method {m.Identifier}"),
                            m.GetLocation()
                            )
                        );
                    continue;
                }
                
            }
        }
    }
}
