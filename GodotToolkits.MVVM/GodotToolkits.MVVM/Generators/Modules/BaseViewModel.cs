using GeneratorUtils;
using Microsoft.CodeAnalysis;
using ProjectInfo = GeneratorUtils.ProjectInfo;

namespace GodotToolkits.MVVM.Generators.Modules;

[Generator(LanguageNames.CSharp)]
public sealed class BaseViewModel : IIncrementalGenerator
{
	public const string ClassName = "BaseViewModel";
	public static readonly string Namespace = ProjectInfo.RootNamespace;

	public static string BuildCode()
	{
		return $@"{AttributeStringBuild.GeneratedTitle}

namespace {Namespace};
using global::System;

{AttributeStringBuild.GeneratedCode(
	$"{ProjectInfo.RootNamespace}.Generators.Modules.BaseViewModel", ProjectInfo.Version)}
public abstract class {ClassName}
{{
}}";
	}

	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		context.RegisterPostInitializationOutput(post =>
		{
			post.AddSource($"{ClassName}.g.cs", BuildCode());
		});
	}
}
