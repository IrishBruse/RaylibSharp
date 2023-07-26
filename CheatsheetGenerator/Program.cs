namespace CheatsheetGenerator;

using System;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal class Program
{
    private static void Main(string[] args)
    {
        SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText("../RaylibSharp/Raylib.cs"));

        CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();
        ClassDeclarationSyntax classNode = (ClassDeclarationSyntax)root.Members.First();

        SyntaxTriviaList trivias = classNode.GetLeadingTrivia();
        SyntaxTrivia xmlCommentTrivia = trivias.FirstOrDefault(t => t.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia));
        SyntaxNode? xml = xmlCommentTrivia.GetStructure();
        Console.WriteLine(xml);

        CSharpCompilation compilation = CSharpCompilation.Create("test", syntaxTrees: new[] { tree });
        INamedTypeSymbol classSymbol = compilation.GlobalNamespace.GetTypeMembers("C").Single();
        string? docComment = classSymbol.GetDocumentationCommentXml();
        Console.WriteLine(docComment);
    }
}
