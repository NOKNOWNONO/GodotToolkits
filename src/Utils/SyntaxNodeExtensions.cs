using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Utils;

public static class SyntaxNodeExtensions
{
	public static string? GetNamespace(this SyntaxNode node)
	{
		var parent = node.Parent;
		return parent switch
		{
			null => null,
			NamespaceDeclarationSyntax nds => nds.Name.ToString(),
			FileScopedNamespaceDeclarationSyntax fsnds => fsnds.Name.ToString(),
			_ => GetNamespace(parent),
		};
	}

	public static (
		NamespaceDeclarationSyntax?,
		FileScopedNamespaceDeclarationSyntax?
	) GetNamespaceNode(this SyntaxNode node)
	{
		return (
			GetNamespaceDeclaration(node),
			GetFileScopedNamespaceDeclaration(node)
		);
	}

	public static NamespaceDeclarationSyntax? GetNamespaceDeclaration(
		this SyntaxNode node
	)
	{
		var parent = node.Parent;
		return parent switch
		{
			null => null,
			NamespaceDeclarationSyntax nds => nds,
			_ => GetNamespaceDeclaration(parent),
		};
	}

	public static List<UsingDirectiveSyntax> GetUsings(this SyntaxNode node)
	{
		var usings = new List<UsingDirectiveSyntax>();
		usings.AddRange(node.ChildNodes().OfType<UsingDirectiveSyntax>());

		return usings;
	}

	public static FileScopedNamespaceDeclarationSyntax? GetFileScopedNamespaceDeclaration(
		this SyntaxNode node
	)
	{
		var parent = node.Parent;
		return parent switch
		{
			null => null,
			FileScopedNamespaceDeclarationSyntax fsnds => fsnds,
			_ => GetFileScopedNamespaceDeclaration(parent),
		};
	}

	public static bool HasAttribute(this SyntaxNode node, string attributeName)
	{
		return node.DescendantNodes()
			.OfType<AttributeSyntax>()
			.Any(a => a.Name.ToString() == attributeName);
	}

	public static bool HasNode(this SyntaxNode node, SyntaxKind kind)
	{
		return node.ChildNodes().Any(n => n.IsKind(kind));
	}
}
