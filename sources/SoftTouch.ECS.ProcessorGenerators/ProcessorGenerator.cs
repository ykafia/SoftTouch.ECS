using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
                .Where(t => t.AllInterfaces.Any(i => i.OriginalDefinition.ToString().Contains("SoftTouch.ECS.IProcessor")))
                .Where(t => t.TypeKind != TypeKind.Interface)
                .Where(t => t?.BaseType?.Name != "Processor")
                .ToList();
            
            context.AddSource("listProcessors.g.cs",$"/*{string.Join("\n",processors.Select(p => p.Name + "-" + p.BaseType.Name))}*/");
            
            foreach (var processor in processors)
            {
                var writer = new CodeWriter();
                //var children = rootNodes.First(r => );
                var types = new List<ClassDeclarationSyntax>();
                foreach(var rn in rootNodes)
                    FindTypeDeclarations(context, processor.OriginalDefinition.ToString(), rn,types);
                var declaration = types.First();

                var processorInterface = declaration?.BaseList?.Types.First().Type as GenericNameSyntax;
                var world = processorInterface?.TypeArgumentList.Arguments.First() as IdentifierNameSyntax;
                var queries = processorInterface.TypeArgumentList.Arguments.OfType<GenericNameSyntax>().ToList();

                if(world == null)
                {
                    var desc = new DiagnosticDescriptor("YKA", "error generating source", "","processor",DiagnosticSeverity.Error,false);
                    context.ReportDiagnostic(Diagnostic.Create(desc, null));
                }
                


                foreach(var import in declaration.SyntaxTree.GetRoot().ChildNodes().OfType<UsingDirectiveSyntax>())
                    writer.Append(import.ToFullString());

                writer.AppendLine("");
                writer.Append("namespace ").Append(processor.ContainingNamespace.ToString()).AppendLine(";");

                writer
                    .Append("public partial class ")
                    .AppendLine(processor.Name)
                    .AppendLine("{")
                    .Indent();
                
                writer.Append("public ").Append(world.Identifier.ToString()).AppendLine(" World { get; set; }");
                for (int i = 0; i < queries.Count; i++)
                {
                    if(queries[i] != null && queries[i].Identifier != null)
                        writer.Append("public ").Append(queries[i].ToString()).Append(' ').Append(queries[i].Identifier.ToString()).Append(i+1).AppendLine(" { get; set; }");
                }

                writer
                    .Append("public void WithWorld(")
                    .Append(world.Identifier.ToString())
                    .AppendLine(" w)")
                    .AppendLine("{")
                    .Indent();
                writer.AppendLine("World = w;");
                for (int i = 0; i < queries.Count; i++)
                {
                    writer.Append("Query").Append(i+1).AppendLine(" = new();");
                    writer.Append("Query").Append(i+1).AppendLine(".WithWorld(w);");
                }
                writer.Dedent()
                    .AppendLine("}")
                    .Dedent()
                    .AppendLine("}");

                context.AddSource($"{processor.Name}.gen.cs", writer.ToString());
            }
            var x = 0;

        }


        public void FindTypeDeclarations(GeneratorExecutionContext context, string name, SyntaxNode node, List<ClassDeclarationSyntax>  types, string nsp = null)
        {
            if(node is NamespaceDeclarationSyntax nds)
            {
                nsp = nds.Name.ToString();
            }
            if (node is FileScopedNamespaceDeclarationSyntax fsnds)
            { 
                nsp = fsnds.Name.ToString();
            }
            if (node is ClassDeclarationSyntax tds)
            {
                if(nsp + "." + tds.Identifier.ToString() == name)
                    types.Add(tds);
            }

            foreach (var n in node.ChildNodes())
            {
                FindTypeDeclarations(context, name, n, types,nsp);
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