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
        
        
        // context.AddSource("Query.g.cs", code.ToString());
    }

    public static string GenerateValueTuplesComponents()
    {
        var code = new CodeWriter();
        code.WriteLine("using CommunityToolkit.HighPerformance.Buffers;")
                        .WriteEmptyLines(2)
                        .WriteLine("namespace SoftTouch.ECS.Core.Queries;")
                        .WriteEmptyLines(3);
        
        for(int i = 1; i < 17; i++)
        {
            // code.WriteLine("public struct")
        }
        
        return code.ToString();

    }

}