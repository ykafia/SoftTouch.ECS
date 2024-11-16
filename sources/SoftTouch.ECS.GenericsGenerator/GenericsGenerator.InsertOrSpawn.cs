using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace SoftTouch.ECS.GenericsGenerator;


public partial class GenericsGenerator
{
    public void GenerateInsertOrSpawn(IncrementalGeneratorInitializationContext context)
    {
        var builder = new StringBuilder();

        builder.AppendLine("using SoftTouch.ECS.Storage;")
            .AppendLine("namespace SoftTouch.ECS;")
            .AppendLine("public partial class WorldCommands")
            .AppendLine("{");

        for(int i = 2; i < 16; i += 1)
        {
            var generics = Enumerable.Range(2, i);
            builder
                .AppendLine($"public void InsertOrSpawn<{string.Join(", ", generics.Select(x => $"T{x}"))}>(Entity entity, {string.Join(", ", generics.Select(x => $"in T{x} component{x}"))})")
                .AppendLine("{")
                .AppendLine();
        }
    }
    
}