﻿using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SoftTouch.ECS.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class GenericsAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "UniqueTypes";
    private static readonly LocalizableString Title = "Duplicate type parameters";
    private static readonly LocalizableString MessageFormat = "Type parameter '{0}' is used more than once";
    private static readonly LocalizableString Description = "Generic types parameters must be unique.";
    private const string Category = "Usage";
    private readonly static DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [Rule];

    public override void Initialize(AnalysisContext context)
    {
        context.EnableConcurrentExecution();
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.Analyze | GeneratedCodeAnalysisFlags.ReportDiagnostics);
        context.RegisterSyntaxNodeAction(AnalyzeProcessorTypes, SyntaxKind.ClassDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeFunctionGenerics, SyntaxKind.MethodDeclaration);
        context.RegisterSyntaxNodeAction(AnalyzeLambdaGenerics, SyntaxKind.SimpleLambdaExpression);
        context.RegisterSyntaxNodeAction(AnalyzeLambdaGenerics, SyntaxKind.ParenthesizedLambdaExpression);
        context.RegisterSyntaxNodeAction(AnalyzeTopLevelStatements, SyntaxKind.GlobalStatement);
    }
    private static void AnalyzeProcessorTypes(SyntaxNodeAnalysisContext context)
    {

        var classDeclaration = (ClassDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;
        var classSymbol = semanticModel.GetDeclaredSymbol(classDeclaration);

        if (classSymbol == null)
            return;

        var baseType = classSymbol.BaseType;
        while (baseType != null)
        {
            if (baseType.Name.StartsWith("Processor") && baseType.ContainingNamespace.ToString() == "SoftTouch.ECS.Processors")
            {
                foreach (var q in baseType.TypeArguments)
                {
                    // context.ReportDiagnostic(Diagnostic.Create(Rule, classDeclaration.GetLocation(), q.ContainingNamespace.ToString()));
                    if (q.Name.StartsWith("Query") && q.ContainingNamespace.ToString() == "SoftTouch.ECS.Querying")
                    {
                        // Write logic here
                        var query = (INamedTypeSymbol)q;
                        var typeArguments = query.TypeArguments.Select(x => x.ToString()).ToList();
                        var duplicateTypeArguments = typeArguments.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToList();

                        if (duplicateTypeArguments.Any())
                        {
                            var typeArgumentSyntaxes = classDeclaration.DescendantNodes()
                                .OfType<TypeArgumentListSyntax>()
                                .SelectMany(tal => tal.Arguments)
                                .ToList();

                            foreach (var duplicate in duplicateTypeArguments)
                            {
                                var locations = query.TypeArguments
                                    .Where(x => x.ToString() == duplicate)
                                    .Select(x => typeArgumentSyntaxes.FirstOrDefault(tas => semanticModel.GetTypeInfo(tas).Type?.ToString() == duplicate)?.GetLocation())
                                    .Where(loc => loc != null);

                                foreach (var location in locations.Reverse())
                                {
                                    context.ReportDiagnostic(Diagnostic.Create(Rule, location, duplicate));
                                }
                            }
                        }

                    }
                }
                return;
            }
            baseType = baseType.BaseType;
        }
    }
    private static void AnalyzeFunctionGenerics(SyntaxNodeAnalysisContext context)
    {
        var methodDeclaration = (MethodDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;
        var methodSymbol = semanticModel.GetDeclaredSymbol(methodDeclaration);

        if (methodSymbol == null)
            return;

        if (methodSymbol.Parameters.Length > 0 && methodSymbol.Parameters.All(parameter => parameter.Type.AllInterfaces.Any(i => i.ToDisplayString() == "SoftTouch.ECS.Querying.IWorldQuery")))
        {
            foreach(var parameter in methodSymbol.Parameters)
            {
                if (parameter.Type.Name.StartsWith("Query") && parameter.Type.ContainingNamespace.ToString() == "SoftTouch.ECS.Querying" && parameter.Type is INamedTypeSymbol p)
                {
                    var duplicates = p.TypeArguments.GroupBy(x => x.ToDisplayString()).Select(group => new { Name = group.Key, Count = group.Count() }).Where( g => g.Count > 1);
                    if (duplicates.Any())
                    {
                        
                        foreach (var dup in duplicates)
                        {
                            var syntax = methodDeclaration.ParameterList.Parameters.First(x => semanticModel.GetTypeInfo(x.Type!).Type?.ToDisplayString() == parameter.Type.ToDisplayString());
                            var sdup = ((INamedTypeSymbol)semanticModel.GetTypeInfo(syntax.Type!).Type!).TypeArguments.First(x => x.ToDisplayString() == dup.Name);
                            context.ReportDiagnostic(Diagnostic.Create(Rule, syntax.GetLocation(), sdup.ToDisplayString()));
                        }
                    }
                }
            }
            // context.ReportDiagnostic(Diagnostic.Create(Rule, methodDeclaration.GetLocation(), string.Join(", ", methodSymbol.Parameters.SelectMany(x => x.Type.Interfaces).Select(x => x.ToDisplayString()))));
        }

    }

    private static void AnalyzeLambdaGenerics(SyntaxNodeAnalysisContext context)
    {
        var lambdaExpression = (LambdaExpressionSyntax)context.Node;
        var semanticModel = context.SemanticModel;
        var lambdaSymbol = semanticModel.GetSymbolInfo(lambdaExpression).Symbol as IMethodSymbol;

        if (lambdaSymbol == null)
            return;

        if (lambdaSymbol.Parameters.Length > 0 && lambdaSymbol.Parameters.All(parameter => parameter.Type.AllInterfaces.Any(i => i.ToDisplayString() == "SoftTouch.ECS.Querying.IWorldQuery")))
        {
            foreach (var parameter in lambdaSymbol.Parameters)
            {
                if (parameter.Type.Name.StartsWith("Query") && parameter.Type.ContainingNamespace.ToString() == "SoftTouch.ECS.Querying" && parameter.Type is INamedTypeSymbol p)
                {
                    var duplicates = p.TypeArguments.GroupBy(x => x.ToDisplayString()).Select(group => new { Name = group.Key, Count = group.Count() }).Where(g => g.Count > 1);
                    if (duplicates.Any())
                    {
                        foreach (var dup in duplicates)
                        {
                            var syntax = lambdaExpression.DescendantNodes().OfType<ParameterSyntax>().First(x => semanticModel.GetTypeInfo(x.Type!).Type?.ToDisplayString() == parameter.Type.ToDisplayString());
                            var sdup = ((INamedTypeSymbol)semanticModel.GetTypeInfo(syntax.Type!).Type!).TypeArguments.First(x => x.ToDisplayString() == dup.Name);
                            context.ReportDiagnostic(Diagnostic.Create(Rule, syntax.GetLocation(), sdup.ToDisplayString()));
                        }
                    }
                }
            }
        }
    }

    private static void AnalyzeTopLevelStatements(SyntaxNodeAnalysisContext context)
    {
        var globalStatement = (GlobalStatementSyntax)context.Node;
        var semanticModel = context.SemanticModel;

        if (globalStatement.Statement is LocalFunctionStatementSyntax localFunction)
        {
            var methodSymbol = semanticModel.GetDeclaredSymbol(localFunction);

            if (methodSymbol == null)
                return;

            if (methodSymbol.Parameters.Length > 0 && methodSymbol.Parameters.All(parameter => parameter.Type.AllInterfaces.Any(i => i.ToDisplayString() == "SoftTouch.ECS.Querying.IWorldQuery")))
            {
                foreach (var parameter in methodSymbol.Parameters)
                {
                    if (parameter.Type.Name.StartsWith("Query") && parameter.Type.ContainingNamespace.ToString() == "SoftTouch.ECS.Querying" && parameter.Type is INamedTypeSymbol p)
                    {
                        var duplicates = p.TypeArguments.GroupBy(x => x.ToDisplayString()).Select(group => new { Name = group.Key, Count = group.Count() }).Where(g => g.Count > 1);
                        if (duplicates.Any())
                        {
                            foreach (var dup in duplicates)
                            {
                                var syntax = localFunction.ParameterList.Parameters.First(x => semanticModel.GetTypeInfo(x.Type!).Type?.ToDisplayString() == parameter.Type.ToDisplayString());
                                var sdup = ((INamedTypeSymbol)semanticModel.GetTypeInfo(syntax.Type!).Type!).TypeArguments.First(x => x.ToDisplayString() == dup.Name);
                                context.ReportDiagnostic(Diagnostic.Create(Rule, syntax.GetLocation(), sdup.ToDisplayString()));
                            }
                        }
                    }
                }
            }
        }
    }

}

