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

namespace SoftTouch.ECS.ProcessorGenerators
{
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
            code.WriteLine($"namespace {projectAssembly.Name}.Bundles;")
                .WriteLine("using SoftTouch.ECS;");
            if (context.SyntaxReceiver is MethodsWithAttributesSR mw)
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
                        var name = symbol.GetAttributes().First().ConstructorArguments.First().Value?.ToString();
                        if (!string.IsNullOrEmpty(name))
                        {
                            CodeWriter? subCode = null!;
                            if (!dict.TryGetValue(name, out subCode))
                            {
                                subCode = new();
                                dict[name] = subCode;
                            }
                            if (!string.IsNullOrEmpty(name))
                            {
                                subCode.WriteLine($"public struct {name}Bundle : IBundle")
                                .OpenBlock()
                                .WriteLine("public App AddBundleElements(App app)")
                                .OpenBlock()
                                .WriteLine($"app.AddProcessor<Main>(Processor.From({m.Parent.}{m.Identifier}))")
                                .CloseAllBlocks();
                                // subCode.Write($"public static string {m.Identifier.Text} = \"")
                                //     .Write(string.Join(", ", symbol.GetAttributes().SelectMany(x => x.ConstructorArguments).Select(n => n.Value)))
                                //     .Write(string.Join(", ", symbol.GetAttributes().SelectMany(ad => ad.NamedArguments).Select(n => $"{n.Key}, {n.Value}")))
                                //     .Write("\";")
                                //     .WriteEmptyLines(1);
                            }
                        }
                    }
                }
            }
            code.CloseAllBlocks();
            context.AddSource("Bundless.g.cs", code.ToString());
        }

    }



}