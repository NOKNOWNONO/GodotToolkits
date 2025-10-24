using GeneratorUtils;
using Microsoft.CodeAnalysis;
using ProjectInfo = GeneratorUtils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class ObservableProperty : IIncrementalGenerator
{
	public const string AttributeName = "ObservableProperty";
	public const string ClassName = "ObservablePropertyAttribute";
	public static readonly string Namespace = ProjectInfo.RootNamespace;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

[AttributeUsage(AttributeTargets.Field)]
{AttributeStringBuild.GeneratedCode(
	$"{ProjectInfo.RootNamespace}.Generators.Modules.ObservableProperty"
	, ProjectInfo.Version
)}
public class {ClassName} : Attribute
{{
}}
";
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(post =>
		{
			post.AddSource($"{ClassName}.g.cs", BuildCode());
		});
	}
}
