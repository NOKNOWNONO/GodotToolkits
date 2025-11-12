using System.Text;
using GodotToolkits.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generator.Generators;

using System;

[Generator(LanguageNames.CSharp)]
public sealed class ViewGenerator : IIncrementalGenerator
{
	public static readonly string GeneratedCode =
		AttributeStringBuild.GeneratedCode(
			$"{ProjectInfo.Title}.Generators.ViewGenerator",
			ProjectInfo.MvvmVersion
		);

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var classNodes = context
			.SyntaxProvider.CreateSyntaxProvider(
				predicate: static (s, _) => s is ClassDeclarationSyntax,
				transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
			)
			.Where(c => c.HasAttribute(Common.ViewAttributeName));

		context.RegisterSourceOutput(classNodes, GenerateCode);
	}

	private void GenerateCode(
		SourceProductionContext context,
		ClassDeclarationSyntax node
	)
	{
		var className = node.Identifier.Text;
		var namespaceName = node.GetNamespace();

		var classBuilder = new StringBuilder();
		classBuilder.AppendLine(
			AttributeStringBuild.GeneratedTitle(
				nameof(ViewGenerator),
				ProjectInfo.MvvmVersion,
				DateTime.Now
			)
		);
		if (namespaceName is not null)
			classBuilder.AppendLine($"namespace {namespaceName};");
		classBuilder.AppendLine();
		classBuilder.AppendLine($"partial class {className} {{");
		classBuilder.AppendLine(GetInitializeComponentsComment());
		classBuilder.AppendLine($"\t{GeneratedCode}");
		classBuilder.AppendLine("\tpartial void InitializeComponents();");
		classBuilder.AppendLine();
		classBuilder.AppendLine(GetInitializeBindingsComment());
		classBuilder.AppendLine($"\t{GeneratedCode}");
		classBuilder.AppendLine("\tpartial void InitializeBindings();");
		classBuilder.AppendLine("}");

		context.AddSource($"{className}View.g.cs", classBuilder.ToString());
	}

	public static string GetInitializeComponentsComment()
	{
		return @"
	/// <summary>
	/// Auto-generated view class.
	/// 用于初始化组件,在Godot中推荐在_Ready()函数中调用InitializeComponents()函数。
	/// </summary>
	";
	}

	public static string GetInitializeBindingsComment()
	{
		return @"
	/// <summary>
	/// Auto-generated view class.
	/// 用于初始化绑定,在Godot中推荐在_Ready()函数中调用InitializeBindings()函数。
	/// </summary>
	";
	}
}
