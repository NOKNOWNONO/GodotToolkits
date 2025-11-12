using System;
using System.Linq;
using System.Text;
using GodotToolkits.Utils;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectInfo = Utils.ProjectInfo;

namespace GodotToolkits.MVVM.Generator.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class ObservablePropertyGenerator : IIncrementalGenerator
{
	public static readonly string GeneratedCode =
		AttributeStringBuild.GeneratedCode(
			$"{ProjectInfo.Title}.Generators.ObservablePropertyGenerator",
			ProjectInfo.MvvmVersion
		);

	public const string FullDictionary =
		"global::System.Collections.Generic.Dictionary";

	public const string FullAction = "global::System.Action";

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		var classNodes = context
			.SyntaxProvider.CreateSyntaxProvider(
				predicate: static (s, _) => s is ClassDeclarationSyntax,
				transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
			)
			.Where(static c => c.HasNode(SyntaxKind.FieldDeclaration))
			.Where(static c =>
				c.ChildNodes()
					.OfType<FieldDeclarationSyntax>()
					.Any(static f =>
						f.HasAttribute(Common.ObservablePropertyAttributeName)
					)
			)
			.Combine(context.CompilationProvider);

		context.RegisterSourceOutput(classNodes, GenerateCode);
	}

	private void GenerateCode(
		SourceProductionContext context,
		(ClassDeclarationSyntax Left, Compilation Right) source
	)
	{
		var (node, compilation) = source;
		var className = node.Identifier.Text;
		var namespaceName = node.GetNamespace();
		var semanticModel = compilation.GetSemanticModel(node.SyntaxTree);
		var fields = node.ChildNodes()
			.OfType<FieldDeclarationSyntax>()
			.Where(n => n.HasAttribute(Common.ObservablePropertyAttributeName));

		var classBuilder = new StringBuilder();
		classBuilder.AppendLine(
			AttributeStringBuild.GeneratedTitle(
				nameof(ObservablePropertyGenerator),
				ProjectInfo.MvvmVersion,
				DateTime.Now
			)
		);
		if (namespaceName is not null)
			classBuilder.AppendLine($"namespace {namespaceName};");
		classBuilder.AppendLine("using global::System.Linq;");
		classBuilder.AppendLine($"partial class {className}");
		classBuilder.AppendLine("{");
		foreach (var field in fields)
		{
			var typeInfo = semanticModel.GetTypeInfo(field.Declaration.Type);
			var typeSymbol = typeInfo.Type;
			if (typeSymbol == null)
			{
				var declared =
					semanticModel.GetDeclaredSymbol(
						field.Declaration.Variables.First()
					) as IFieldSymbol;
				typeSymbol = declared?.Type;
			}

			var globalName =
				typeSymbol?.ToDisplayString(
					SymbolDisplayFormat.FullyQualifiedFormat
				) ?? field.Declaration.Type.ToString();

			var fieldName = field.Declaration.Variables.First().Identifier.Text;
			classBuilder.Append(BuildBindingPooling(fieldName, globalName, 1));
			classBuilder.Append(BuildBindingFunc(fieldName, globalName, 1));
			classBuilder.Append(BuildUnBindingFunc(fieldName, globalName, 1));
			classBuilder.Append(
				BuildUnBindingAllFunc(fieldName, globalName, 1)
			);
			classBuilder.Append(BuildSetPropertyFunc(fieldName, globalName, 1));
			classBuilder.Append(BuildProperty(fieldName, globalName, 1));
			classBuilder.Append(
				BuildOnPropertyChangingOneArgPartialFunc(
					fieldName,
					globalName,
					1
				)
			);
			classBuilder.Append(
				BuildOnPropertyChangingTwoArgPartialFunc(
					fieldName,
					globalName,
					1
				)
			);
			classBuilder.Append(
				BuildOnPropertyChangedPartialFunc(fieldName, globalName, 1)
			);
		}

		classBuilder.AppendLine("}");
		context.AddSource(
			$"{className}Properties.g.cs",
			classBuilder.ToString()
		);
	}

	public static string BuildIndent(int count) => new('\t', count);

	public static string GetBackBindingPoolingName(string fieldName) =>
		$"G_back_{fieldName}_binding_pooling_G";

	public static string BuildBindingPooling(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}private readonly {FullDictionary}<object, {FullAction}<{fieldType}>> {GetBackBindingPoolingName(fieldName)} = [];

";
	}

	public static string BuildUnBindingFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{AttributeStringBuild.ExcludeFromCodeCoverage}
{BuildIndent(indentCount)}public void Unbinding{GetPropertyName(fieldName)}(object observer) {{
{BuildIndent(indentCount + 1)}{GetBackBindingPoolingName(fieldName)}.Remove(observer);
{BuildIndent(indentCount)}}}

";
	}

	public static string BuildUnBindingAllFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{AttributeStringBuild.ExcludeFromCodeCoverage}
{BuildIndent(indentCount)}private void UnbindingAll{GetPropertyName(fieldName)}() {{
{BuildIndent(indentCount + 1)}{GetBackBindingPoolingName(fieldName)}.Clear();
{BuildIndent(indentCount)}}}

";
	}

	public static string BuildBindingFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{AttributeStringBuild.ExcludeFromCodeCoverage}
{BuildIndent(indentCount)}public void Binding{GetPropertyName(fieldName)}(object observer,{FullAction}<{fieldType}> onChangeDo) {{
{BuildIndent(indentCount + 1)}{GetBackBindingPoolingName(fieldName)}.Add(observer, onChangeDo);
{BuildIndent(indentCount)}}}

";
	}

	public static string BuildSetPropertyFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		var propertyName = GetPropertyName(fieldName);
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{AttributeStringBuild.ExcludeFromCodeCoverage}
{BuildIndent(indentCount)}public void Set{propertyName}({fieldType} value,object? observer) {{
{BuildIndent(indentCount + 1)}if ({fieldName} == value) return;
{BuildIndent(indentCount + 1)}On{propertyName}Changing(value);
{BuildIndent(indentCount + 1)}On{propertyName}Changing({fieldName}, value);
{BuildIndent(indentCount + 1)}{fieldName} = value;
{BuildIndent(indentCount + 1)}On{propertyName}Changed({fieldName});
{BuildIndent(indentCount + 1)}var actions = {GetBackBindingPoolingName(fieldName)}
{BuildIndent(indentCount + 2)}.Values
{BuildIndent(indentCount + 2)}.Where(a => a.Target != observer)
{BuildIndent(indentCount + 2)}.ToList();
{BuildIndent(indentCount + 1)}
{BuildIndent(indentCount + 1)}foreach (var action in actions)
{BuildIndent(indentCount + 2)}action(value);
{BuildIndent(indentCount + 1)}
{BuildIndent(indentCount)}}}

";
	}

	public static string BuildProperty(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		var propertyName = GetPropertyName(fieldName);
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{AttributeStringBuild.ExcludeFromCodeCoverage}
{BuildIndent(indentCount)}public {fieldType} {propertyName} {{
{BuildIndent(indentCount + 1)}get => {fieldName};
{BuildIndent(indentCount + 1)}{AttributeStringBuild.MemberNotNull(fieldName)}
{BuildIndent(indentCount + 1)}set => Set{propertyName}(value, null);
{BuildIndent(indentCount)}}}

";
	}

	public static string BuildOnPropertyChangingTwoArgPartialFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}partial void On{GetPropertyName(fieldName)}Changing({fieldType} oldValue, {fieldType} newValue);

";
	}

	public static string BuildOnPropertyChangingOneArgPartialFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}partial void On{GetPropertyName(fieldName)}Changing({fieldType} newValue);

";
	}

	private static string BuildOnPropertyChangedPartialFunc(
		string fieldName,
		string fieldType,
		byte indentCount
	)
	{
		return $@"
{BuildIndent(indentCount)}{GeneratedCode}
{BuildIndent(indentCount)}{GetOnPropertyChanged(GetPropertyName(fieldName), fieldType)}

";
	}

	public static string GetOnPropertyChanged(
		string propertyName,
		string fieldType
	)
	{
		return $"partial void On{propertyName}Changed({fieldType} newValue);";
	}

	public static string GetPropertyName(string fieldName)
	{
		clear_start:
		if (fieldName.StartsWith("_"))
		{
			fieldName = fieldName.Substring(1);
			goto clear_start;
		}

		var propertyName =
			fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
		return propertyName;
	}
}
