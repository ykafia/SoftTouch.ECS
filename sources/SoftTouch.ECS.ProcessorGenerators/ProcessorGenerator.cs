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
    [Generator]
    public class ProcessorGenerator : ISourceGenerator
    {

        public void Initialize(GeneratorInitializationContext context)
        {
            // #if DEBUG
            //            if (!Debugger.IsAttached)
            //            {
            //                Debugger.Launch();
            //            }
            // #endif
            //Debug.WriteLine("Initalize code generator");
        }
        public void Execute(GeneratorExecutionContext context)
        {
            var projectAssembly = context.Compilation.Assembly;
            var rootNodes = context.Compilation.SyntaxTrees.Select(tree => tree.GetRoot());
            var semantics = context.Compilation.SyntaxTrees.Select(tree => context.Compilation.GetSemanticModel(tree));


            var processors =
                GetAllTypes(projectAssembly.GlobalNamespace)
                .Where(t => t.AllInterfaces.Any(i => i.OriginalDefinition.ToString().Contains("SoftTouch.ECS.Processors.IProcessor")))
                .Where(t => t.TypeKind != TypeKind.Interface)
                .Where(t => t?.BaseType?.Name != "Processor")
                .ToList();

            context.AddSource("listProcessors.g.cs", $"/*{string.Join("\n", processors.Select(p => p.Name + "-" + p.BaseType?.Name))}*/");

            foreach (var processor in processors)
            {
                var writer = new CodeWriter();
                //var children = rootNodes.First(r => );
                var types = new List<ClassDeclarationSyntax>();
                foreach (var rn in rootNodes)
                    FindTypeDeclarations(context, processor.OriginalDefinition.ToString(), rn, types);
                var declaration = types.First();

                var processorInterface = declaration?.BaseList?.Types.First().Type as GenericNameSyntax;
                var world = processorInterface?.TypeArgumentList.Arguments.First() as IdentifierNameSyntax;
                var queries = processorInterface?.TypeArgumentList.Arguments.OfType<GenericNameSyntax>().ToList();

                if (world == null)
                {
                    var desc = new DiagnosticDescriptor("YKA", "error generating source", "", "processor", DiagnosticSeverity.Error, false);
                    context.ReportDiagnostic(Diagnostic.Create(desc, null));
                }



                foreach (var import in declaration?.SyntaxTree.GetRoot().ChildNodes().OfType<UsingDirectiveSyntax>() ?? [])
                    writer.Write(import.ToFullString());

                writer.WriteEmptyLines(1);
                writer.Write("namespace ").Write(processor.ContainingNamespace.ToString()).WriteLine(";");

                writer
                    .Write("public partial class ")
                    .WriteLine(processor.Name)
                    .OpenBlock();

                writer.Write("public ").Write(world?.Identifier.ToString() ?? "").WriteLine(" World { get; set; }");
                for (int i = 0; i < queries?.Count; i++)
                {
                    if (queries[i] != null && queries[i].Identifier != null)
                        writer.Write("public ").Write(queries[i].ToString()).Write(" ").Write(queries[i].Identifier.ToString()).Write($"{i + 1}").WriteLine(" { get; set; }");
                }

                writer
                    .Write("public void WithWorld(")
                    .Write(world?.Identifier.ToString() ?? "")
                    .WriteLine(" w)")
                    .OpenBlock();
                writer.WriteLine("World = w;");
                for (int i = 0; i < queries?.Count; i++)
                {
                    writer.Write("Query").Write($"{i + 1}").WriteLine(" = new();");
                    writer.Write("Query").Write($"{i + 1}").WriteLine(".WithWorld(w);");
                }
                writer.CloseAllBlocks();

                context.AddSource($"{processor.Name}.gen.cs", writer.ToString());
            }
        }


        public void FindTypeDeclarations(GeneratorExecutionContext context, string name, SyntaxNode node, List<ClassDeclarationSyntax> types, string nsp = null!)
        {
            if (node is NamespaceDeclarationSyntax nds)
            {
                nsp = nds.Name.ToString();
            }
            if (node is FileScopedNamespaceDeclarationSyntax fsnds)
            {
                nsp = fsnds.Name.ToString();
            }
            if (node is ClassDeclarationSyntax tds)
            {
                if (nsp + "." + tds.Identifier.ToString() == name)
                    types.Add(tds);
            }

            foreach (var n in node.ChildNodes())
            {
                FindTypeDeclarations(context, name, n, types, nsp);
            }
        }

        private static IEnumerable<ITypeSymbol> GetAllTypes(INamespaceSymbol root)
        {
            foreach (var namespaceOrTypeSymbol in root.GetMembers())
            {
                if (namespaceOrTypeSymbol is INamespaceSymbol @namespace) foreach (var nested in GetAllTypes(@namespace)) yield return nested;

                else if (namespaceOrTypeSymbol is ITypeSymbol type)
                {
                    foreach (var t in type.GetTypeMembers())
                        yield return t;
                    yield return type;
                }
            }
        }
    }



}