using Microsoft.CodeAnalysis;


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
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
                .WriteEmptyLines(2)
                .WriteLine("namespace SoftTouch.ECS.Core.Queries;")
                .WriteEmptyLines(3);
        foreach (var p in Permutations())
        {
        
            code.WriteLine($"public partial struct Query<{string.Join(", ", p.Select(x => $"T{x}"))}>")
            .WriteLine($"{string.Join("\n",p.Select(x => $"where T{x} : struct, I{x}"))}")
            .OpenBlock()
            .CloseAllBlocks()
            .WriteEmptyLines(3);
                
        }
        context.AddSource("Query.g.cs", code.ToString());
    }


    public static IEnumerable<string[]> Permutations()
    {
        yield return new[] { "With" };
        yield return new[] { "WithAll" };
        yield return new[] { "Without" };
        yield return new[] { "With", "Without" };
    }


}