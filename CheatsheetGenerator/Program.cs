namespace CheatsheetGenerator;

using System;
using System.Text.RegularExpressions;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

internal sealed partial class Program
{
    private static void Main(string[] args)
    {
        string[] files = Directory.GetFiles("../RaylibSharp/", "*.cs", SearchOption.AllDirectories);

        foreach (string item in files)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(File.ReadAllText(item));

            CompilationUnitSyntax root = (CompilationUnitSyntax)tree.GetRoot();
            Walk(root);
        }
    }

    private static void Walk(SyntaxNode node)
    {
        foreach (SyntaxNode child in node.ChildNodes())
        {
            if (child.HasLeadingTrivia)
            {
                SyntaxTriviaList doc = child.GetLeadingTrivia();
                foreach (SyntaxTrivia item in doc)
                {
                    if (item.IsKind(SyntaxKind.SingleLineDocumentationCommentTrivia))
                    {
                        NewMethod(child, Vector2AddReplace().Replace(doc.ToString(), "$1").Trim());
                    }
                }
            }
            Walk(child);
        }
    }

    private static void NewMethod(SyntaxNode child, string doc)
    {
        switch (child)
        {
            case MethodDeclarationSyntax method:
            Console.Write(method.Identifier);
            Console.WriteLine(" // " + doc);
            break;

            default:
            // Console.WriteLine("Unhandled: " + child.Kind());
            break;
        }
    }

    [GeneratedRegex(@"/// <summary> (.*) </summary>")] private static partial Regex Vector2AddReplace(); // Vector2Add(delta, -1.0f / camera.Zoom);

}
